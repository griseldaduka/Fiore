using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiore.Models
{
    public class ProductOrderDetails
    {
        [Key]
        [Column(Order = 1)]
        public int? OrderId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int? ProductId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; } 

    }
}
