using Lab3_WebApp.Data.Models;

namespace Lab3_WebApp.ViewModels
{
    public class SearchViewModel
    {
        public string? SearchTerm { get; set; }
        public List<FoundItem> FoundItems { get; set; } = new();
        public List<LostItem> LostItems { get; set; } = new();
    }
}