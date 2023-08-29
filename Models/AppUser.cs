using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace VirtualShop.Models
{
    public class AppUser : SharedModel
    {
        [Required(ErrorMessage = "Please enter a your name.")]
        [StringLength(100, ErrorMessage = "The product name must be between {2} and {1} characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        public string EncryptedPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }
        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Required(ErrorMessage = "Confirm password")]
        public string ConfirmPassword { get; set; }
        
        // Navigation property for roles
        public List<AppRole> Roles { get; set; }
        public string CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }

    public class AppUserViewModel: SharedModel
    {
        public string Name { get; set; }

        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
