using Microsoft.AspNetCore.Mvc.RazorPages;
using lost_found_web.Services;
using lost_found_web.Models;

namespace lost_found_web.Pages.Search
{
    public class IndexModel : PageModel
    {
        private readonly ILostFoundService _lostFoundService;

        public IndexModel(ILostFoundService lostFoundService)
        {
            _lostFoundService = lostFoundService;
        }

        public string SearchTerm { get; set; } = string.Empty;
        public IEnumerable<LostItem> LostItems { get; set; } = new List<LostItem>();
        public IEnumerable<FoundItem> FoundItems { get; set; } = new List<FoundItem>();

        public async Task OnGetAsync(string searchTerm)
        {
            SearchTerm = searchTerm ?? string.Empty;
            
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                LostItems = await _lostFoundService.SearchLostItemsAsync(SearchTerm);
                FoundItems = await _lostFoundService.SearchFoundItemsAsync(SearchTerm);
            }
            else
            {
                LostItems = new List<LostItem>();
                FoundItems = new List<FoundItem>();
            }
        }
    }
}
