using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lost_found_web.Services;
using lost_found_web.Models;

namespace lost_found_web.Pages.LostItems
{
    public class EditModel : PageModel
    {
        private readonly ILostFoundService _lostFoundService;

        public EditModel(ILostFoundService lostFoundService)
        {
            _lostFoundService = lostFoundService;
        }

        [BindProperty]
        public LostItem LostItem { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var lostItem = await _lostFoundService.GetLostItemByIdAsync(id);
            if (lostItem == null)
            {
                return NotFound();
            }

            LostItem = lostItem;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _lostFoundService.UpdateLostItemAsync(LostItem);
            if (result == null)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
