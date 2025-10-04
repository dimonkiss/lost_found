using lost_found_web.Models;

namespace lost_found_web.Services
{
    public class LostFoundService : ILostFoundService
    {
        private readonly List<LostItem> _lostItems = new();
        private readonly List<FoundItem> _foundItems = new();
        private int _lostItemIdCounter = 1;
        private int _foundItemIdCounter = 1;

        public LostFoundService()
        {
            // Add some sample data
            _lostItems.Add(new LostItem
            {
                Id = _lostItemIdCounter++,
                ItemName = "iPhone 13",
                Description = "Black iPhone 13 with a blue case. Lost near the library entrance.",
                LostLocation = "University Library",
                LostDate = DateTime.Now.AddDays(-2),
                ContactInfo = "john.doe@university.edu",
                CreatedAt = DateTime.Now.AddDays(-2)
            });

            _foundItems.Add(new FoundItem
            {
                Id = _foundItemIdCounter++,
                ItemName = "Student ID Card",
                Description = "Student ID card found in the cafeteria. Name: Jane Smith.",
                FoundLocation = "Cafeteria",
                FoundDate = DateTime.Now.AddDays(-1),
                ContactInfo = "security@university.edu",
                CreatedAt = DateTime.Now.AddDays(-1)
            });
        }

        // Lost Items
        public async Task<IEnumerable<LostItem>> GetLostItemsAsync()
        {
            return await Task.FromResult(_lostItems.Where(x => !x.IsFound).OrderByDescending(x => x.CreatedAt));
        }

        public async Task<LostItem?> GetLostItemByIdAsync(int id)
        {
            return await Task.FromResult(_lostItems.FirstOrDefault(x => x.Id == id));
        }

        public async Task<LostItem> AddLostItemAsync(LostItem item)
        {
            item.Id = _lostItemIdCounter++;
            item.CreatedAt = DateTime.Now;
            _lostItems.Add(item);
            return await Task.FromResult(item);
        }

        public async Task<LostItem?> UpdateLostItemAsync(LostItem item)
        {
            var existingItem = _lostItems.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem != null)
            {
                var index = _lostItems.IndexOf(existingItem);
                _lostItems[index] = item;
                return await Task.FromResult(item);
            }
            return null;
        }

        public async Task<bool> DeleteLostItemAsync(int id)
        {
            var item = _lostItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _lostItems.Remove(item);
                return await Task.FromResult(true);
            }
            return false;
        }

        public async Task<IEnumerable<LostItem>> SearchLostItemsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetLostItemsAsync();

            var term = searchTerm.ToLower();
            return await Task.FromResult(_lostItems.Where(x => !x.IsFound && 
                (x.ItemName.ToLower().Contains(term) || 
                 x.Description.ToLower().Contains(term) || 
                 x.LostLocation.ToLower().Contains(term))).OrderByDescending(x => x.CreatedAt));
        }

        // Found Items
        public async Task<IEnumerable<FoundItem>> GetFoundItemsAsync()
        {
            return await Task.FromResult(_foundItems.Where(x => !x.IsClaimed).OrderByDescending(x => x.CreatedAt));
        }

        public async Task<FoundItem?> GetFoundItemByIdAsync(int id)
        {
            return await Task.FromResult(_foundItems.FirstOrDefault(x => x.Id == id));
        }

        public async Task<FoundItem> AddFoundItemAsync(FoundItem item)
        {
            item.Id = _foundItemIdCounter++;
            item.CreatedAt = DateTime.Now;
            _foundItems.Add(item);
            return await Task.FromResult(item);
        }

        public async Task<FoundItem?> UpdateFoundItemAsync(FoundItem item)
        {
            var existingItem = _foundItems.FirstOrDefault(x => x.Id == item.Id);
            if (existingItem != null)
            {
                var index = _foundItems.IndexOf(existingItem);
                _foundItems[index] = item;
                return await Task.FromResult(item);
            }
            return null;
        }

        public async Task<bool> DeleteFoundItemAsync(int id)
        {
            var item = _foundItems.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _foundItems.Remove(item);
                return await Task.FromResult(true);
            }
            return false;
        }

        public async Task<IEnumerable<FoundItem>> SearchFoundItemsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetFoundItemsAsync();

            var term = searchTerm.ToLower();
            return await Task.FromResult(_foundItems.Where(x => !x.IsClaimed && 
                (x.ItemName.ToLower().Contains(term) || 
                 x.Description.ToLower().Contains(term) || 
                 x.FoundLocation.ToLower().Contains(term))).OrderByDescending(x => x.CreatedAt));
        }
    }
}
