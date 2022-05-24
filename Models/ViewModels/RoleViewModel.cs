using System.ComponentModel.DataAnnotations;

namespace Fiore.Models.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Role")]
        public string Name { get; set; }
    }
}
