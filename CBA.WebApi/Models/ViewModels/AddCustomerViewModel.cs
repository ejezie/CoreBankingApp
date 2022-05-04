using System;
using System.ComponentModel.DataAnnotations;
using CBA.Core.Enums;

namespace CBA.Core.Models.ViewModels
{
    public class AddCustomerViewModel
    {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public Gender Gender { get; set; }
            [Required]
            public Roles Role { get; set; } 
    }
}
