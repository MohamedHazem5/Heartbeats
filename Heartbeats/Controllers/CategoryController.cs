using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Heartbeats.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        // Add Category
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (!ModelState.IsValid) return View(category);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Edit Category
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid) return View(category);
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Delete Category
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
