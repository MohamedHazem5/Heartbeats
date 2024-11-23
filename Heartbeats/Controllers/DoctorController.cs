using DAL.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Dtos;

namespace Heartbeats.Controllers
{
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Doctor/GetBySpecialtyId/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var doctorList = await _context.Doctors
                .Where(x => x.SpecialtyId == id)
                .Include(d => d.Specialty)
                .Include(d => d.User)
                .ToListAsync();

            var doctors = doctorList.Select(
                doc => new DoctorDetailsDto
                {
                    Id = doc.Id,
                    UserName = doc.User.Name,
                    ImageUrl = doc.User.ImageUrl,
                    Bio = doc.Bio,
                    SpecialtyName = doc.Specialty.Name
                }).ToList();

            return View(doctors);
        }

        [HttpGet]
        public IActionResult Register(int id)
        {
            ViewBag.Specialties = new SelectList(_context.Specialties.ToList(), "Id", "Name");

            return View(new DoctorDto() { UserId = id });
        }

        [HttpPost]
        public async Task<IActionResult> Register(DoctorDto doctorDto)
        {
            if (!ModelState.IsValid) return View(doctorDto);

            await _context.Doctors.AddAsync(new Doctor
            {
                Bio = doctorDto.Bio,
                SpecialtyId = doctorDto.SpecialtyId,
                UserId = doctorDto.UserId
            });
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}