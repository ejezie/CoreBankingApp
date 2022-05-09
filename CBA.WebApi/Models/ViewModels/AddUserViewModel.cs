using System;
using System.ComponentModel.DataAnnotations;
using CBA.Core.Enums;

namespace CBA.CORE.Models.ViewModels
{
    public class AddUserViewModel
    {
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "{0} should consist of only alphabets")]
        [StringLength(50, ErrorMessage = "{0} should not exceed {1} characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "{0} should consist of only alphabets")]
        [StringLength(50, ErrorMessage = "{0} should not exceed {1} characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }
        //public string PasswordHash { get; set; }
    }
}
