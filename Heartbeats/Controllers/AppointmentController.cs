using DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Dtos;

namespace Heartbeats.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AppointmentController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type.EndsWith("nameidentifier"))!.Value);
            var appointments = _context.Appointments
                .Where(app => app.PatientId == userId || app.DoctorId == userId)
                .Include(appoin => appoin.Patient)
                .Include(appoin => appoin.Doctor.User)
                .ToList();
            return View(appointments);
        }

        [HttpPost]
        public IActionResult Create(int doctorId, DateTime appointmentDate)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type.EndsWith("nameidentifier"))!.Value);
            //var isExist = _context.Appointments.Any(app => app.DoctorId == doctorId && app.PatientId == userId && app.ScheduleAt.Date == appointmentDate.Date);
            //if (isExist)
            //{
            //    ModelState.AddModelError("appointmentDate", "You already have an appointment with this doctor at this date");
            //    return RedirectToAction("GetDoctorBySpecialty", "Doctor");
            //}
            var appointment = new Appointment
            {
                DoctorId = doctorId,
                PatientId = userId,
                ScheduleAt = appointmentDate,
                CreatedAt = DateTime.Now
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return RedirectToAction(actionName: "Index"); // Redirect to a confirmation or appointments list page
        }

        [HttpPost]
        public IActionResult Edit(int id, DateTime scheduleAt)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                appointment.ScheduleAt = scheduleAt;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}