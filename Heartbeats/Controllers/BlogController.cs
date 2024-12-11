using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Heartbeats.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(blogs);
        }
        [AllowAnonymous]

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();

            var blog = await _context.Blogs.FindAsync(id);
            return View(blog);
        }
        [AllowAnonymous]

        public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return View("SearchResults", new List<Blog>()); // Empty result view
            }

            var blogs = await _context.Blogs
                .Where(b => b.Title.Contains(query) || b.Author.Contains(query))
                .ToListAsync();

            return View("SearchResults", blogs);
        }
        [AllowAnonymous]
        public async Task<IActionResult> ByCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            var blogs = await _context.Blogs
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();

            ViewBag.CategoryName = category.Name; // Display category name in the view
            return View("CategoryBlogs", blogs);
        }
        public async Task<IActionResult> List()
        {
            var blogs = await _context.Blogs.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(blogs);
        }
        // Add Blog
        [HttpPost]
        public async Task<IActionResult> Add(Blog blog)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();

            if (!ModelState.IsValid) return View(blog);
            blog.CreatedAt = DateTime.Now;
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        // Edit Blog
        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();

            var oldblog = await _context.Blogs.FindAsync(blog.BlogId);
            if (oldblog == null) return View(blog);
            if (!ModelState.IsValid) return View(blog);

            // Update the properties of the existing entity
            oldblog.CreatedAt = oldblog.CreatedAt;
            oldblog.Title = blog.Title;
            oldblog.Description = blog.Description;
            oldblog.Author = blog.Author;
            oldblog.CategoryId = blog.CategoryId;
            oldblog.ImageUrl = blog.ImageUrl;
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

        // Delete Blog
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("List");
        }

    }
}
