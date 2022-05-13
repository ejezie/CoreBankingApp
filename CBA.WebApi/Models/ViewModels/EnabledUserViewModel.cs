using System;
using System.Collections.Generic;

namespace CBA.CORE.Models.ViewModels
{
    public class EnabledUserViewModel : AddUserViewModel
    {
        public EnabledUserViewModel()
        {
            Users = new List<string>();
        }

        public List<string> Users { get; set; }
    }
}
