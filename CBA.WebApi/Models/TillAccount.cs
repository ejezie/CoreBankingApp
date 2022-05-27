using System;
using System.ComponentModel.DataAnnotations;

namespace CBA.CORE.Models
{
    public class TillAccount
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "A Teller must be selected")]
        public string UserId { get; set; }
        //public virtual ApplicationUser User { get; set; }


        [Required(ErrorMessage = "The Till Account must be selected")]
        public int GlAccountID { get; set; }

        public virtual GLAccount GlAccount { get; set; }
    }
}
