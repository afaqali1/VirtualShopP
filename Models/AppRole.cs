using System.ComponentModel.DataAnnotations;

namespace VirtualShop.Models
{
    public class AppRole : SharedModel
    {

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50)]
        public string Name { get; set; }

        // Navigation property for users with this role
        public List<AppUser> Users { get; set; }
    }
}

