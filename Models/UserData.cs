

using System.ComponentModel.DataAnnotations;

namespace DevExtreme.Asp.Template.Gallery.Models
{
    public class UserData
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[\d\w._-]+@[\d\w._-]+\.[\w]+$", ErrorMessage = "Email is invalid")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty ;
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "'Password' and 'Confirm Password' do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
