using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureShop.Data;
using SecureShop.Models;

namespace SecureShop.Controllers;

[Authorize(Roles = "Customer")]
public class OrdersController : Controller
{
	private readonly ApplicationDbContext _dbContext;
	private readonly UserManager<IdentityUser> _userManager;

	public OrdersController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)
	{
		_dbContext = dbContext;
		_userManager = userManager;
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create(int productId, int quantity)
	{
		if (quantity < 1 || quantity > 1000)
		{
			ModelState.AddModelError("quantity", "Quantity must be between 1 and 1000.");
			return RedirectToAction("Details", "Products", new { id = productId });
		}

		var product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == productId);
		if (product == null)
		{
			return NotFound();
		}

		var userId = _userManager.GetUserId(User)!;
		var order = new Order
		{
			UserId = userId,
			Items = new List<OrderItem>
			{
				new OrderItem
				{
					ProductId = product.Id,
					Quantity = quantity,
					UnitPrice = product.Price
				}
			}
		};
		order.Total = order.Items.Sum(i => i.UnitPrice * i.Quantity);

		_dbContext.Orders.Add(order);
		await _dbContext.SaveChangesAsync();

		return RedirectToAction("Index", "Products");
	}
}


