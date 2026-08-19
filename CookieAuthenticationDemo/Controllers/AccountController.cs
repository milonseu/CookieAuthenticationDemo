using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace CookieAuthenticationDemo.Controllers
{
    public class AccountController : Controller
    { // ==============================
        // GET: /Account/Login
        // ==============================
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        // ==============================
        // POST: /Account/Login
        // ==============================
        [HttpPost]
        public async Task<IActionResult> Login(
            string email,
            string password)
        {
            // Demo User
            // Real project-এ Database থেকে check করবে
            if (email == "admin@gmail.com" &&
                password == "123456")
            {
                // ==============================
                // Claims
                // ==============================

                var claims = new List<Claim>
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        "1"
                    ),

                    new Claim(
                        ClaimTypes.Name,
                        "Admin User"
                    ),

                    new Claim(
                        ClaimTypes.Email,
                        email
                    ),

                    new Claim(
                        ClaimTypes.Role,
                        "Admin"
                    )
                };


                // ==============================
                // ClaimsIdentity
                // ==============================

                var identity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                );


                // ==============================
                // ClaimsPrincipal
                // ==============================

                var principal = new ClaimsPrincipal(identity);


                // ==============================
                // Create Authentication Cookie
                // ==============================

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal
                );


                // Login successful
                return RedirectToAction("Dashboard");
            }


            // Login failed
            ViewBag.Error = "Invalid email or password";

            return View();
        }


        // ==============================
        // Dashboard
        // ==============================

        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }


        // ==============================
        // Admin Panel
        // ==============================

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }


        // ==============================
        // Access Denied
        // ==============================

        public IActionResult AccessDenied()
        {
            return View();
        }


        // ==============================
        // Logout
        // ==============================

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Login");
        }
    }
}
