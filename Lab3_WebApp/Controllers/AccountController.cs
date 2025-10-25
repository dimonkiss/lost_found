using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Lab3_WebApp.Data;
using Lab3_WebApp.ViewModels;
using Lab3_WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Lab3_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Цей метод ініціює перенаправлення на Google
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("ExternalLoginCallback", new { returnUrl })
            };
            return Challenge(properties, "Google");
        }

        // Цей метод обробляє відповідь від Google
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = "/")
        {
            // Отримуємо інформацію про користувача з тимчасового cookie, який створив Google
            var result = await HttpContext.AuthenticateAsync("Identity.External");
            if (result?.Succeeded != true)
            {
                return RedirectToAction("Login");
            }

            var externalId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ExternalId == externalId);

            if (user != null)
            {
                // === І ТУТ ===
                await HttpContext.SignOutAsync("Identity.External");
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.ExternalId),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Username", user.Username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { IsPersistent = true };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return LocalRedirect(returnUrl);
            }

            // Якщо користувач новий, показуємо йому сторінку реєстрації
            var model = new RegisterViewModel
            {
                Email = result.Principal.FindFirstValue(ClaimTypes.Email) ?? "",
                FullName = result.Principal.FindFirstValue(ClaimTypes.Name) ?? ""
            };
            return View("Register", model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            // Користувач не повинен потрапляти на цю сторінку напряму.
            // Він має пройти через процес зовнішнього входу.
            // Тому перенаправляємо його на початок процесу.
            return RedirectToAction("Login");
        }

        // Метод для обробки POST-запиту з форми реєстрації
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Знову отримуємо дані від Google, щоб шахрай не підмінив їх
            var result = await HttpContext.AuthenticateAsync("Identity.External");
            if (result?.Succeeded != true)
            {
                return RedirectToAction("Login");
            }

            model.Email = result.Principal.FindFirstValue(ClaimTypes.Email) ?? model.Email;
            model.FullName = result.Principal.FindFirstValue(ClaimTypes.Name) ?? model.FullName;

            if (ModelState.IsValid)
            {
                var usernameExists = await _context.Users.AnyAsync(u => u.Username == model.Username);
                if (usernameExists)
                {
                    ModelState.AddModelError("Username", "Це ім'я користувача вже зайняте.");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    ExternalId = result.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
                    Username = model.Username,
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Після успішної реєстрації логінимо користувача
                return await ExternalLoginCallback();
            }

            return View(model);
        }

        // Метод виходу
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        // Сторінка профілю
        [Authorize]
        public IActionResult Profile()
        {
            return View(User.Claims);
        }
    }
}