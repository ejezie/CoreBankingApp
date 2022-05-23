using System;
using System.ComponentModel.DataAnnotations;
using CBA.Core.Utility;
using static CBA.CORE.Enums.Enums;
//using static CBA.CORE.Enums.Enums;

namespace CBA.CORE.Models
{
    public class GLCategory
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Input GL Category Name")]
        public string Name { get; set; }

        public long Code { get; set; }

        [Required(ErrorMessage = "Main GL Category must be selected")]
        [Display(Name = "Main GL Category")]
        public MainGLCategory MainGLCategory { get; set; }

        [Required(ErrorMessage = "Enter Description")]
        public string Description { get; set; }
    }
}
