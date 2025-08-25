using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureApp.Data;
using SecureApp.Models;
using SecureApp.Security;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SecureApp.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FinancialRecordsController : ControllerBase
	{
		private readonly AppDbContext _db;
		private readonly IEncryptionService _crypto;

		public FinancialRecordsController(AppDbContext db, IEncryptionService crypto)
		{
			_db = db;
			_crypto = crypto;
		}

		public record CreateRequest([Required] Guid OwnerUserId, [Required, MaxLength(256)] string Label, [Required, MaxLength(32)] string Currency, [Required] decimal Amount, [MaxLength(32)] string? MaskedPan);

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateRequest request)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var exists = await _db.Users.AnyAsync(u => u.Id == request.OwnerUserId);
			if (!exists) return BadRequest("Invalid owner");

			var payloadJson = $"{{\"label\":\"{Escape(request.Label)}\",\"currency\":\"{Escape(request.Currency)}\",\"amount\":{request.Amount},\"maskedPan\":\"{Escape(request.MaskedPan ?? string.Empty)}\"}}";
			var plaintext = Encoding.UTF8.GetBytes(payloadJson);
			var ad = request.OwnerUserId.ToByteArray();
			var blob = _crypto.Encrypt(plaintext, ad);

			// Split hmac for storage
			var macLen = 32;
			var encLen = blob.Length - macLen;
			var enc = new byte[encLen];
			var mac = new byte[macLen];
			Buffer.BlockCopy(blob, 0, enc, 0, encLen);
			Buffer.BlockCopy(blob, encLen, mac, 0, macLen);

			var rec = new FinancialRecord
			{
				OwnerUserId = request.OwnerUserId,
				EncryptedPayload = enc,
				PayloadHmac = mac
			};
			_db.FinancialRecords.Add(rec);
			await _db.SaveChangesAsync();
			return CreatedAtAction(nameof(Get), new { id = rec.Id }, new { rec.Id });
		}

		[HttpGet("{id:guid}")]
		public async Task<IActionResult> Get(Guid id)
		{
			var rec = await _db.FinancialRecords.FirstOrDefaultAsync(r => r.Id == id);
			if (rec == null) return NotFound();
			var combined = new byte[rec.EncryptedPayload.Length + rec.PayloadHmac.Length];
			Buffer.BlockCopy(rec.EncryptedPayload, 0, combined, 0, rec.EncryptedPayload.Length);
			Buffer.BlockCopy(rec.PayloadHmac, 0, combined, rec.EncryptedPayload.Length, rec.PayloadHmac.Length);
			var (pt, ok) = _crypto.Decrypt(combined, rec.OwnerUserId.ToByteArray());
			if (!ok) return StatusCode(409, "Integrity check failed");
			var json = Encoding.UTF8.GetString(pt);
			return Ok(new { id = rec.Id, ownerUserId = rec.OwnerUserId, payload = json });
		}

		private static string Escape(string input)
		{
			return input.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("<", "");
		}
	}
}


