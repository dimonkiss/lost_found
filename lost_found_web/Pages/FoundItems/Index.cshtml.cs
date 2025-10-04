using Microsoft.AspNetCore.Mvc.RazorPages;
using lost_found_web.Services;
using lost_found_web.Models;

namespace lost_found_web.Pages.FoundItems
{
    public class IndexModel : PageModel
    {
        private readonly ILostFoundService _lostFoundService;

        public IndexModel(ILostFoundService lostFoundService)
        {
            _lostFoundService = lostFoundService;
        }

        public IEnumerable<FoundItem> FoundItems { get; set; } = new List<FoundItem>();

        public async Task OnGetAsync()
        {
            FoundItems = await _lostFoundService.GetFoundItemsAsync();
        }
    }
}
