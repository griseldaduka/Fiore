using Fiore.Models;
using Fiore.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Fiore.Data;

public class ApplicationUser : IdentityUser
{
    //[Required]
    //[StringLength(225, ErrorMessage = "First Name must be shorter")]
    public string FirstName { get; set; }
    //[Required]
    //[StringLength(225, ErrorMessage = "Last Name must be shorter")]
    public string LastName { get; set; }
    public  List<Order> Orders { get; set; }
    public List<Complaint> Complaints { get; set; }
}