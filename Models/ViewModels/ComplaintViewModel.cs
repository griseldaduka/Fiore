using System.ComponentModel.DataAnnotations;

namespace Fiore.Models.ViewModels
{
    public class ComplaintViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
