using System.ComponentModel.DataAnnotations;

namespace Clean.Core.Models.ViewModels
{
    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Bio")]
        public string Bio { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string CurrentPassword { get; set; } = string.Empty;

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}