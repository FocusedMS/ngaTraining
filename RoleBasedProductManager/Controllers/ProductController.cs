using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Data;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var productList = await _dbContext.Products.ToListAsync();
            return View(productList);
        }

        // GET: Product/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name,Price,Description")] Product newProduct)
        {
            if (ModelState.IsValid)
            {
                newProduct.CreatedDate = DateTime.Now;
                _dbContext.Add(newProduct);
                await _dbContext.SaveChangesAsync();
                
                TempData["SuccessMessage"] = $"Product \"{newProduct.Name}\" has been successfully created!";
                return RedirectToAction(nameof(Index));
            }
            return View(newProduct);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var existingProduct = await _dbContext.Products.FindAsync(productId);
            if (existingProduct == null)
            {
                return NotFound();
            }
            return View(existingProduct);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int productId, [Bind("Id,Name,Price,Description,CreatedDate")] Product updatedProduct)
        {
            if (productId != updatedProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    updatedProduct.ModifiedDate = DateTime.Now;
                    _dbContext.Update(updatedProduct);
                    await _dbContext.SaveChangesAsync();
                    
                    TempData["SuccessMessage"] = $"Product \"{updatedProduct.Name}\" has been successfully updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(updatedProduct.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(updatedProduct);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            var productToDelete = await _dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (productToDelete == null)
            {
                return NotFound();
            }

            return View(productToDelete);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int productId)
        {
            var productToRemove = await _dbContext.Products.FindAsync(productId);
            if (productToRemove != null)
            {
                _dbContext.Products.Remove(productToRemove);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int productId)
        {
            return _dbContext.Products.Any(p => p.Id == productId);
        }
    }
}
