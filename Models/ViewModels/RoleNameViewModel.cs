using System.ComponentModel.DataAnnotations;

namespace Fiore.Models.ViewModels
{
    public class RoleNameViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}
