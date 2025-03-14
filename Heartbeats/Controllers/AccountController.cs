﻿using DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Dtos;

namespace Heartbeats.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginDto());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid) return View(loginDto);
                var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email);
                if (user == null) return RedirectToAction("Login", "Account");
                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);
                if (!result.Succeeded) return View(loginDto);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View(loginDto);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return View(registerDto);

            if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email))
            {
                ModelState.AddModelError("Email", "The Email is Taken");
                return View(registerDto);
            }
            if (registerDto.Password.Length < 6)
            {
                ModelState.AddModelError("Email", "PasswordTooShort,PasswordRequiresLower,PasswordRequiresUpper");
                return View(registerDto);
            }

            var user = new User
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                Name = registerDto.Name,
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return View(registerDto);

            await _userManager.AddToRoleAsync(user, registerDto.Role);

            await _signInManager.SignInAsync(user, true);

            return registerDto.Role.Equals("Doctor", StringComparison.OrdinalIgnoreCase) ? RedirectToAction("Register", "Doctor", new { id = user.Id }) : RedirectToAction("Index", "Home");
            //return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            ProfileDto profileDto;

            if (await _userManager.IsInRoleAsync(user, "Doctor"))
            {
                var doctor = await _context.Doctors
                    .Include(d => d.User)
                    .Include(d => d.Specialty)
                    .Include(d => d.Appointments.Where(a => a.ScheduleAt >= DateTime.Now))
                    .ThenInclude(app=>app.Patient)
                    .FirstOrDefaultAsync(d => d.UserId == user.Id);

                if (doctor == null) return NotFound();

                profileDto = new ProfileDto
                {
                    Id = doctor.Id,
                    Name = doctor.User.Name,
                    Appointments = doctor.Appointments.ToList(),
                    Bio=doctor.Bio,
                    Specialty = doctor.Specialty,
                    ImageUrl = doctor.User.ImageUrl
                };
            }
            else
            {
                profileDto = new ProfileDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    ImageUrl = user.ImageUrl,
                };
            }

            return View(profileDto);
        }

        [HttpGet]

        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();
            var dto = new EditProfileDto()
            {
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                ImageUrl = user.ImageUrl,
                UserName = user.UserName,

            };
            return View(dto);
        }

        [HttpPost]

        public async Task<IActionResult> EditProfile(EditProfileDto dto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            user.Name = dto.Name;
            user.PhoneNumber = dto.PhoneNumber;
            user.ImageUrl = dto.ImageUrl;
            user.Email = dto.Email;
            user.UserName = dto.UserName;

            await _userManager.UpdateAsync(user);
            _context.SaveChanges();
            return RedirectToAction("Profile", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}