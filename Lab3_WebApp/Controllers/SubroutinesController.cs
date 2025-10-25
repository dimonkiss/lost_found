using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Lab3_WebApp.Data;
using System.Threading.Tasks;
using Lab3_WebApp.Data.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Lab3_WebApp.ViewModels;
using System.Linq;

namespace Lab3_WebApp.Controllers
{
    [Authorize]
    public class SubroutinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubroutinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- СТВОРЕННЯ ЗНАХІДКИ ---
        public IActionResult AddFoundItem() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFoundItem(FoundItem model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _context.Users.FirstAsync(u => u.ExternalId == userId);
                model.ApplicationUserId = user.Id;

                _context.FoundItems.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("SearchItems");
            }
            return View(model);
        }

        // --- СТВОРЕННЯ ВТРАТИ ---
        public IActionResult AddLostItem() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLostItem(LostItem model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await _context.Users.FirstAsync(u => u.ExternalId == userId);
                model.ApplicationUserId = user.Id;

                _context.LostItems.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("SearchItems");
            }
            return View(model);
        }

        // --- ПОШУК ---
        public async Task<IActionResult> SearchItems(string searchTerm)
        {
            var model = new SearchViewModel { SearchTerm = searchTerm };

            var foundQuery = _context.FoundItems.Include(i => i.ApplicationUser).AsQueryable();
            var lostQuery = _context.LostItems.Include(i => i.ApplicationUser).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                foundQuery = foundQuery.Where(i => i.Title.Contains(searchTerm) || i.Description.Contains(searchTerm));
                lostQuery = lostQuery.Where(i => i.Title.Contains(searchTerm) || i.Description.Contains(searchTerm));
            }

            model.FoundItems = await foundQuery.OrderByDescending(i => i.Date).ToListAsync();
            model.LostItems = await lostQuery.OrderByDescending(i => i.Date).ToListAsync();

            return View(model);
        }
    }
}