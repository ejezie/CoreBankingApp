using System;
using System.ComponentModel.DataAnnotations;

namespace CBA.CORE.Models
{
    public class GLAccount : Account
    {
        [Display(Name = "GL Code")]
        public virtual int Code { get; set; }

        [Display(Name = "GL Category")]
        public virtual GLCategory GLCategory { get; set; }
     
    }
}
