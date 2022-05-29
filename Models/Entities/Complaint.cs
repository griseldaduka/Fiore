using Fiore.Data;
using System.ComponentModel.DataAnnotations;

namespace Fiore.Models.Entities
{
    public class Complaint
    {
        public int Id { get; set; } 
        public string UserId { get; set; }  
        [Required]
        public string Subject { get; set; } 
        [Required]
        public string Description { get; set; }
        [Required]
        public string PhoneNumber { get; set; } 
        [Required]
        public DateTime CmpDate { get; set; }
        public bool Checked { get; set; }   
        public ApplicationUser ApplicationUser { get; set; }
    }
}
