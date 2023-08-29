using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualShop.Models
{
    public class Category : SharedModel
    {
        [Required(ErrorMessage = "Please enter a product name.")]
        [StringLength(100, ErrorMessage = "The product name must be between {2} and {1} characters.", MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a product description.")]
        [StringLength(500, ErrorMessage = "The product description must be between 5 and 500 characters.", MinimumLength = 5)]
        public string Description { get; set; }
        public bool Status{ get; set; }
        public CategoryType Type { get; set; }
        [NotMapped]
        public IFormFile Logo { get; set; }

        [StringLength(200)]
        public string LogoUrl { get; set; }

        public List<Product> Products { get; set; }

        public virtual List<Category> Categories { get; set; }
        //Self Join
        [ScaffoldColumn(false)]
        public string ParentId { get; set; }
        [ForeignKey("ParentId")]
        [ScaffoldColumn(false)]
        public virtual Category Parent { get; set; }


    }

    public enum CategoryType
    {
        Category = 0,
        Brand = 10,
        Blogs = 20
    }

    //public enum CategoryStatus
    //{
    //    UnderVerification = 0,
    //    Active = 10,
    //    Upcoming = 20,
    //    Deactivated = 30
    //}
}
