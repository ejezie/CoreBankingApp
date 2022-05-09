using System;
using System.ComponentModel.DataAnnotations;

namespace CBA.CORE.Models.ViewModels
{
    public class AddRoleViewModel
    {
        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    }
}