using System;
using System.ComponentModel.DataAnnotations;

namespace CBA.CORE.Models.ViewModels
{
    public class CreateCustomerAccountViewModel
    {
        [Required(ErrorMessage = "Account name is required")]
        [RegularExpression(@"^[ a-zA-Z]+$", ErrorMessage = "Account name should only contain characters and white spaces"), MaxLength(40)]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Please select a branch")]
        [Display(Name = "Branch")]
        public int BranchID { get; set; }

        [Required]
        public int CustomerID { get; set; }
    }
}
