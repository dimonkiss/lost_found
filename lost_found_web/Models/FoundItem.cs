using System.ComponentModel.DataAnnotations;

namespace lost_found_web.Models
{
    public class FoundItem
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Item name is required")]
        [StringLength(100, ErrorMessage = "Item name cannot exceed 100 characters")]
        public string ItemName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Location where item was found is required")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string FoundLocation { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Date when item was found is required")]
        public DateTime FoundDate { get; set; }
        
        [Required(ErrorMessage = "Contact information is required")]
        [StringLength(200, ErrorMessage = "Contact information cannot exceed 200 characters")]
        public string ContactInfo { get; set; } = string.Empty;
        
        public string? ImageUrl { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public bool IsClaimed { get; set; } = false;
    }
}
