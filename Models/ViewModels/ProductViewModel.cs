using System.ComponentModel.DataAnnotations;

namespace Fiore.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Product name must be shorter")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        [MaxLength(4000, ErrorMessage = "Product's description must be shorter")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image Name")]
        public string ImageName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Units In Stock")]
        public int UnitsInStock { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }
        public bool IsInCart { get; set; }
        public Category Category { get; set; }  
    }
}
