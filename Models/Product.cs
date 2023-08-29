namespace VirtualShop.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product:SharedModel
    {
        public Product()
        {
            Images = new();
        }

        [Required(ErrorMessage = "Please enter a product name.")]
        [StringLength(100, ErrorMessage = "The product name must be between {2} and {1} characters.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a price.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [ForeignKey("Brand")]
        public string BrandId { get; set; }
        public virtual Category Brand { get; set; }

        [Required(ErrorMessage = "The Stock field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The Stock must be a non-negative number.")]
        public int Stock { get; set; }

        public string CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public virtual List<ProductImage> Images { get; set; }
        

        [Required(ErrorMessage = "Please enter a slug.")]
        [StringLength(100, ErrorMessage = "The slug must be between {2} and {1} characters.", MinimumLength = 2)]
        public string Slug { get; set; } // For SEO-friendly URLs

        [NotMapped]
        public List<IFormFile> Uploads { get; set; }
    }

    public class ProductImage:SharedModel
    {
        public string Url { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Rank { get; set; }
    }

}
