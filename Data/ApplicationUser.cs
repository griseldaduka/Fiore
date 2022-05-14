﻿using Fiore.Models;
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
    public virtual ICollection<Order> Orders { get; set; }
}