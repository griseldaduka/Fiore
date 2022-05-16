using System.ComponentModel.DataAnnotations;

namespace Fiore.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Total Price")]
        public Decimal TotalPrice { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "House Number must be shorter")]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Street name be shorter")]
        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "City name must be shorter")]
        [Display(Name = "City Name")]
        public string CityName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Country name must be shorter")]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
    }
}
