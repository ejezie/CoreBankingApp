using System;
using System.ComponentModel.DataAnnotations;
using CBA.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace CBA.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = " First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = " Last Name")]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [Display(Name = "Enable/Disable User")]
        public UserState UserState { get; set; } = UserState.Enabled;

        //public UserState IsEnable { get; set; } = UserState.isEnabled;

        //public string UserName { get; set; }
        //[Required]
        //public string Email { get; set; }
        //public string NormalizedEmail { get; set; }
        //[Required]
        //[Display(Name = "User Name")]
        //public string UserName { get; set; }
        //public string NormalizedUserName { get; set; }
        //[Required]
        //[Display(Name = "Phone Number")]
        //public string PhoneNumber { get; set; }
        //[Required]
        //[Display(Name = "Confirm Email")]
        //public bool EmailConfirmed { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        //public string SecurityStamp { get; set; }
        //public string PasswordHash { get; set; }
    }
}
