using CmsProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CmsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CmsDbContext _context;

        public CustomersController(CmsDbContext context)
        {
            _context = context;
        }

        // Customer Show (all)
        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        // CustomerSearchById
        // GET: api/Customers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> CustomerSearchById(int id)
        {
            var c = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustId == id);
            return c is null ? NotFound() : c;
        }

        // CustomerSearchByUserName
        // GET: api/Customers/by-username/{userName}
        [HttpGet("by-username/{userName}")]
        public async Task<ActionResult<Customer>> CustomerSearchByUserName(string userName)
        {
            var c = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.CustUserName == userName);
            return c is null ? NotFound() : c;
        }

        // Customer Add -> Make Password Encrypted (hashed)
        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<string>> AddCustomer(CustomerCreateDto dto)
        {
            if (await _context.Customers.AnyAsync(x => x.CustId == dto.CustId))
                return Conflict("custId already exists.");
            if (!string.IsNullOrWhiteSpace(dto.CustUserName) &&
                await _context.Customers.AnyAsync(x => x.CustUserName == dto.CustUserName))
                return Conflict("custUserName already exists.");

            var entity = new Customer
            {
                CustId = dto.CustId,
                CustName = dto.CustName,
                CustUserName = dto.CustUserName,
                CustPassword = PasswordHasher.Hash(dto.Password),   // store hash
                City = dto.City,
                State = dto.State,
                Email = dto.Email,
                MobileNo = dto.MobileNo
            };

            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CustomerSearchById), new { id = entity.CustId }, "Customer Created");
        }

        // CustomerAuthentication
        // POST: api/Customers/authenticate
        [HttpPost("authenticate")]
        public async Task<ActionResult<object>> CustomerAuthentication(CustomerAuthDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CustUserName) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("username/password required.");

            var c = await _context.Customers.FirstOrDefaultAsync(x => x.CustUserName == dto.CustUserName);
            if (c is null) return Unauthorized("Invalid credentials.");

            var ok = PasswordHasher.Verify(dto.Password, c.CustPassword);
            if (!ok) return Unauthorized("Invalid credentials.");

            return Ok(new { message = "Authenticated", customerId = c.CustId, userName = c.CustUserName });
        }
    }
}
