using Fiore.Data;

namespace Fiore.Models.Entities
{
    public class Complaint
    {
        public int Id { get; set; } 
        public string UserId { get; set; }  
        public string Description { get; set; }   
        public DateTime CmpDate { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
