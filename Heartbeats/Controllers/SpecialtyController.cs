using DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Heartbeats.Controllers
{
    public class SpecialtyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(bool isAdmin = false, int size = 10, int page = 0)
        {
            if (isAdmin) return View("List", await _context.Specialties.ToListAsync());
            return View(await _context.Specialties.Skip(size * page).Take(size).ToListAsync());
        }

        // Add Speciality
        [HttpPost]
        public async Task<IActionResult> Add(Specialty specialty)
        {
            if (!ModelState.IsValid) return View(specialty);
            await _context.Specialties.AddAsync(specialty);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Edit Speciality
        [HttpPost]
        public async Task<IActionResult> Edit(Specialty specialty)
        {
            if (!ModelState.IsValid) return View(specialty);
            _context.Specialties.Update(specialty);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // Delete Speciality
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var specialty = await _context.Specialties.FindAsync(id);
            if (specialty == null) return NotFound();
            _context.Specialties.Remove(specialty);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}