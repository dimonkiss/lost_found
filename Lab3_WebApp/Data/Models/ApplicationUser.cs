using System.ComponentModel.DataAnnotations;

namespace Lab3_WebApp.Data.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        // Це буде унікальний ідентифікатор від Google
        [Required]
        public string ExternalId { get; set; } = string.Empty;

        // Поля згідно з лабораторною роботою
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }
    }
}