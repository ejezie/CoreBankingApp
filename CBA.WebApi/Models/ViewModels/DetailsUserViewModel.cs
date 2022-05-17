using System;
using System.Collections.Generic;
using CBA.Core.Models;

namespace CBA.CORE.Models.ViewModels
{
    public class DetailsUserViewModel
    {
        public DetailsUserViewModel()
        {
            Roles = new List<string>();
        }
        public ApplicationUser User { get; set; }
        public string PageTitle { get; set; }
        public List<string> Roles { get; set; }   
    }
}
