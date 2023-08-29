using System.ComponentModel.DataAnnotations;

namespace VirtualShop.Models
{
    public class Wishlist:SharedModel
    {
        public string AppUserId { get; set; } // Store the unique user identifier
        public AppUser AppUser { get; set; }
        public List<Product> Products { get; set; }
    }
}
