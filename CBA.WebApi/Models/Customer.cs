using System;
using System.ComponentModel.DataAnnotations;
using CBA.Core.Enums;

namespace CBA.Core.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 30 characters")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(20, ErrorMessage = "Name cannot exceed 30 characters")]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public Roles Role { get; set; }
    }
}
