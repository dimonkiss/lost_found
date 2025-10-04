using lost_found_web.Models;

namespace lost_found_web.Services
{
    public interface ILostFoundService
    {
        // Lost Items
        Task<IEnumerable<LostItem>> GetLostItemsAsync();
        Task<LostItem?> GetLostItemByIdAsync(int id);
        Task<LostItem> AddLostItemAsync(LostItem item);
        Task<LostItem?> UpdateLostItemAsync(LostItem item);
        Task<bool> DeleteLostItemAsync(int id);
        Task<IEnumerable<LostItem>> SearchLostItemsAsync(string searchTerm);

        // Found Items
        Task<IEnumerable<FoundItem>> GetFoundItemsAsync();
        Task<FoundItem?> GetFoundItemByIdAsync(int id);
        Task<FoundItem> AddFoundItemAsync(FoundItem item);
        Task<FoundItem?> UpdateFoundItemAsync(FoundItem item);
        Task<bool> DeleteFoundItemAsync(int id);
        Task<IEnumerable<FoundItem>> SearchFoundItemsAsync(string searchTerm);
    }
}
