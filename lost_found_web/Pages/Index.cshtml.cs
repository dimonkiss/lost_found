using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using lost_found_web.Services;
using lost_found_web.Models;

namespace lost_found_web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ILostFoundService _lostFoundService;

    public IndexModel(ILogger<IndexModel> logger, ILostFoundService lostFoundService)
    {
        _logger = logger;
        _lostFoundService = lostFoundService;
    }

    public IEnumerable<LostItem> RecentLostItems { get; set; } = new List<LostItem>();
    public IEnumerable<FoundItem> RecentFoundItems { get; set; } = new List<FoundItem>();

    public async Task OnGetAsync()
    {
        RecentLostItems = (await _lostFoundService.GetLostItemsAsync()).Take(5);
        RecentFoundItems = (await _lostFoundService.GetFoundItemsAsync()).Take(5);
    }
}