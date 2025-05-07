using System.ComponentModel.DataAnnotations;
using NewsWebsite.Utilities;

namespace Clean.Core.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = ErrorMessages.NameRequired)]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ErrorMessages.InvalidEmail)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessages.PhoneRequired)]
        [Display(Name = "Mobile Phone")]
        [RegularExpression(RegexPatterns.EgyptianMobilePhone, 
            ErrorMessage = ErrorMessages.InvalidEgyptianPhone)]
        public string MobilePhone { get; set; } = string.Empty;

        [Required(ErrorMessage = ErrorMessages.PasswordRequired)]
        [StringLength(100, ErrorMessage = ErrorMessages.PasswordLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = ErrorMessages.PasswordMismatch)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}