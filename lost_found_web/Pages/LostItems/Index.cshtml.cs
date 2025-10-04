using Microsoft.AspNetCore.Mvc.RazorPages;
using lost_found_web.Services;
using lost_found_web.Models;

namespace lost_found_web.Pages.LostItems
{
    public class IndexModel : PageModel
    {
        private readonly ILostFoundService _lostFoundService;

        public IndexModel(ILostFoundService lostFoundService)
        {
            _lostFoundService = lostFoundService;
        }

        public IEnumerable<LostItem> LostItems { get; set; } = new List<LostItem>();

        public async Task OnGetAsync()
        {
            LostItems = await _lostFoundService.GetLostItemsAsync();
        }
    }
}
