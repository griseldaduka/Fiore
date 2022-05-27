using System.ComponentModel.DataAnnotations;

namespace Fiore.Models.ViewModels
{
    public class AddressPostViewModel
    {
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
        [Display(Name = "Image ")]
        public IFormFile Image { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Units In Stock")]
        public int UnitsInStock { get; set; }

        
    }
}
