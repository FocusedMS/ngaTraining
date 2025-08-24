using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureShop.Web.Data;
using SecureShop.Web.Models;
using SecureShop.Web.ViewModels;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _db;
    public ProductsController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var products = await _db.Products.AsNoTracking().OrderBy(p => p.Name).ToListAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(int id)
    {
        var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return NotFound();

        var reviews = await _db.ProductReviews.AsNoTracking()
            .Where(r => r.ProductId == id)
            .OrderByDescending(r => r.CreatedAtUtc)
            .ToListAsync();

        var vm = new ProductDetailsViewModel
        {
            Product = product,
            Reviews = reviews,
            Review = new ReviewInputModel { ProductId = id }
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddReview(ReviewInputModel input)
    {
        if (!ModelState.IsValid)
        {
            var product = await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == input.ProductId);
            var reviews = await _db.ProductReviews.AsNoTracking()
                .Where(r => r.ProductId == input.ProductId)
                .OrderByDescending(r => r.CreatedAtUtc)
                .ToListAsync();

            var vm = new ProductDetailsViewModel
            {
                Product = product ?? new Product(),
                Reviews = reviews,
                Review = input
            };
            return View("Details", vm);
        }

        _db.ProductReviews.Add(new ProductReview
        {
            ProductId = input.ProductId,
            ReviewerName = input.ReviewerName,
            Comment = input.Comment,
            CreatedAtUtc = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Details), new { id = input.ProductId });
    }
}
