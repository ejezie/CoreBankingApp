using System;
using System.ComponentModel.DataAnnotations;
using CBA.Core.Utility;

namespace CBA.CORE.Models
{
    public class Account : BaseEntity
    {
        [Display(Name = "Account Name")]
        public virtual string Name { get; set; }

        public virtual double Balance { get; set; }
    }
}
