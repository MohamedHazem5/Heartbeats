using DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Heartbeats.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }
        // Add Blog
        [HttpPost]
        public async Task<IActionResult> Add(Blog blog)
        {
            if (!ModelState.IsValid) return View(blog);
            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
