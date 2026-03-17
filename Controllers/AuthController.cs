using System.Data;
using Frontend_Exam.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend_Exam.Controllers
{
    public class AuthController : Controller
    {
        public readonly IHttpContextAccessor _contextAccessor;
        public readonly AuthService _authService;

        public AuthController(IHttpContextAccessor contextAccessor, AuthService authService)
        {
            _contextAccessor = contextAccessor;
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _authService.AuthenticateUserAsync(email, password);

            if (result == null || string.IsNullOrEmpty(result.token))
            {
                ViewBag.Error = "Invalid Credentials..!";
                return View();
            }

            // ✅ Store token
            _contextAccessor.HttpContext.Session.SetString("JWTToken", result.token);

            // ✅ Store user info (optional but useful)
            _contextAccessor.HttpContext.Session.SetString("UserName", result.user.name);
            _contextAccessor.HttpContext.Session.SetString("UserEmail", result.user.email);
            _contextAccessor.HttpContext.Session.SetString("UserRole", result.user.role);
            _contextAccessor.HttpContext.Session.SetString("ClinicName", result.user.clinicName);
            _contextAccessor.HttpContext.Session.SetString("ClinicCode", result.user.clinicCode);

            if (result.user.role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (result.user.role.Equals("Patient", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Home");
            }
            else if (result.user.role.Equals("Receptionist", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Receptionist");
            }
            else if (result.user.role.Equals("Doctor", StringComparison.OrdinalIgnoreCase))
            {
                return RedirectToAction("Index", "Doctor");
            }
            else
            {
                // Optional: fallback for unknown roles
                ViewBag.Error = "Unknown role.";
                return View();
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Logout()
        {
            _contextAccessor.HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}
