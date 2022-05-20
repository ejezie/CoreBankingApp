using System;
using System.ComponentModel.DataAnnotations;

namespace CBA.CORE.Models
{
    public class GLAccount
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Input the GL Account Name")]
        [RegularExpression(@"[a-zA-Z ]+$", ErrorMessage = "Must Contain only Characters")]
        [Display(Name = "Account Name")]
        public string AccountName { get; set; }


        [Display(Name = "GL Account Code")]
        public long Code { get; set; }


        [Display(Name = "Account Balance")]
        [DataType(DataType.Currency)]
        public decimal AccountBalance { get; set; }


        [Required(ErrorMessage = "Select a Branch")]
        public int BranchID { get; set; }
        public Branch Branch { get; set; }


        public int GLCategoryID { get; set; }
        public virtual GLCategory GLCategory { get; set; }

    }
}
