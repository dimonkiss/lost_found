using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lost_found_web.Services;
using lost_found_web.Models;

namespace lost_found_web.Pages.FoundItems
{
    public class EditModel : PageModel
    {
        private readonly ILostFoundService _lostFoundService;

        public EditModel(ILostFoundService lostFoundService)
        {
            _lostFoundService = lostFoundService;
        }

        [BindProperty]
        public FoundItem FoundItem { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var foundItem = await _lostFoundService.GetFoundItemByIdAsync(id);
            if (foundItem == null)
            {
                return NotFound();
            }

            FoundItem = foundItem;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _lostFoundService.UpdateFoundItemAsync(FoundItem);
            if (result == null)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
