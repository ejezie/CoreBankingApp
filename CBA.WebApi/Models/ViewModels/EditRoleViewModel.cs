using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CBA.Core.Enums;

namespace CBA.CORE.Models.ViewModels
{
    public class EditRoleViewModel
    {
        //initialise list of users in role to avoid null  eception error

        public EditRoleViewModel()
        {
            Users = new List<string>();
            Claims = new List<string>();

        }

        public string Id { get; set; }

        [Display(Name ="Role Name")]
        [Required(ErrorMessage = "Role Name is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }

        public List<string> Claims { get; set; }

        public State State { get; set; }

    }
}
