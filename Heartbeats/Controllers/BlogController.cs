using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Reflection.Metadata;

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
        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(blogs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            return View(blog);
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
            return RedirectToAction("AdminHome");
        }
        // Edit Blog
        [HttpPost]
        public async Task<IActionResult> Edit(Blog blog)
        {
            if (!ModelState.IsValid) return View(blog);
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminHome");
        }

        // Delete Blog
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("AdminHome");
        }

    }
}
