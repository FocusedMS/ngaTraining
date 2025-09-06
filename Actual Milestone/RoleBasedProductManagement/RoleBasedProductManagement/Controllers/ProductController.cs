using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoleBasedProductManagement.Data;
using RoleBasedProductManagement.Models;
using RoleBasedProductManagement.Services;

namespace RoleBasedProductManagement.Controllers;

[Authorize] // all actions require login
public class ProductsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IPriceProtector _protect;

    public ProductsController(ApplicationDbContext db, IPriceProtector protect)
    {
        _db = db;
        _protect = protect;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var items = await _db.Products.AsNoTracking().ToListAsync();
        foreach (var p in items)
            if (_protect.TryUnprotect(p.ProtectedPrice, out var price))
                p.Price = price;
        return View(items);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return NotFound();
        _protect.TryUnprotect(p.ProtectedPrice, out var price);
        p.Price = price;
        return View(p);
    }

    // Admin: Create
    [Authorize(Roles = "Admin")]
    [HttpGet] public IActionResult Create() => View();

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product model)
    {
        if (!ModelState.IsValid) return View(model);
        model.ProtectedPrice = _protect.Protect(model.Price);
        _db.Add(model);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Product created.";
        return RedirectToAction(nameof(Index));
    }

    // Admin + Manager: Edit
    [Authorize(Roles = "Admin,Manager")]
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return NotFound();
        _protect.TryUnprotect(p.ProtectedPrice, out var price);
        p.Price = price;
        return View(p);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        var p = await _db.Products.FindAsync(id);
        if (p == null) return NotFound();

        p.Name = model.Name;
        p.Description = model.Description;
        p.ProtectedPrice = _protect.Protect(model.Price);

        await _db.SaveChangesAsync();
        TempData["Success"] = "Product updated.";
        return RedirectToAction(nameof(Index));
    }

    // Admin only: Delete
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return NotFound();
        _protect.TryUnprotect(p.ProtectedPrice, out var price);
        p.Price = price;
        return View(p);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var p = await _db.Products.FindAsync(id);
        if (p == null) return NotFound();
        _db.Products.Remove(p);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Product deleted.";
        return RedirectToAction(nameof(Index));
    }
}
