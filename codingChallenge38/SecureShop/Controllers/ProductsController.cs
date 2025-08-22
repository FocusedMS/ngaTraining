using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureShop.Data;
using SecureShop.Models;

namespace SecureShop.Controllers;

public class ProductsController : Controller
{
	private readonly ApplicationDbContext _dbContext;

	public ProductsController(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> Index()
	{
		var products = await _dbContext.Products.AsNoTracking().ToListAsync();
		return View(products);
	}

	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> Details(int id)
	{
		var product = await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
		if (product == null) return NotFound();
		return View(product);
	}
}


