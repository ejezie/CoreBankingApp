using System;
using System.ComponentModel.DataAnnotations;
using CBA.Core.Utility;
using static CBA.CORE.Enums.Enums;

namespace CBA.CORE.Models
{
    public class MainAccount : BaseEntity
    {
        [Display(Name = "Branch Name")]
        [Required(ErrorMessage = "{0} is required")]
        public virtual AccountCategory Name { get; set; }
    }
}
