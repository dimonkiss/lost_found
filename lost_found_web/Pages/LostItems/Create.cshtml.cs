using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lost_found_web.Services;
using lost_found_web.Models;

namespace lost_found_web.Pages.LostItems
{
    public class CreateModel : PageModel
    {
        private readonly ILostFoundService _lostFoundService;

        public CreateModel(ILostFoundService lostFoundService)
        {
            _lostFoundService = lostFoundService;
        }

        [BindProperty]
        public LostItem LostItem { get; set; } = new();

        public IActionResult OnGet()
        {
            LostItem.LostDate = DateTime.Now;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _lostFoundService.AddLostItemAsync(LostItem);
            return RedirectToPage("./Index");
        }
    }
}
