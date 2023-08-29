using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualShop.Models
{
    public class Cart:SharedModel
    {
        
        public decimal TotalCost { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Product> Products { get; set; }
    }

}
