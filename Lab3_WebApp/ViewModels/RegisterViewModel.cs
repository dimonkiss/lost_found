using System.ComponentModel.DataAnnotations;

namespace Lab3_WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ім'я користувача є обов'язковим")]
        [StringLength(50, ErrorMessage = "Ім'я користувача не може перевищувати 50 символів")]
        [Display(Name = "Ім'я користувача (унікальне)")]
        public string Username { get; set; } = string.Empty;

        // Ці поля будуть заповнені автоматично з Google, але показані користувачу
        [Display(Name = "Повне ім'я")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Номер телефону є обов'язковим")]
        [Phone(ErrorMessage = "Невірний формат номеру телефону")]
        [RegularExpression(@"^\+380\d{9}$", ErrorMessage = "Введіть номер у форматі +380xxxxxxxxx")]
        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}