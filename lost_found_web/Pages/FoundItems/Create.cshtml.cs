using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lost_found_web.Services;
using lost_found_web.Models;

namespace lost_found_web.Pages.FoundItems
{
    public class CreateModel : PageModel
    {
        private readonly ILostFoundService _lostFoundService;

        public CreateModel(ILostFoundService lostFoundService)
        {
            _lostFoundService = lostFoundService;
        }

        [BindProperty]
        public FoundItem FoundItem { get; set; } = new();

        public IActionResult OnGet()
        {
            FoundItem.FoundDate = DateTime.Now;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _lostFoundService.AddFoundItemAsync(FoundItem);
            return RedirectToPage("./Index");
        }
    }
}
