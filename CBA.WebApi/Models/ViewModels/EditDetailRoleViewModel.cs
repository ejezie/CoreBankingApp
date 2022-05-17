using System;
using System.Collections.Generic;
using CBA.Core.Enums;
using System.Security.Claims;

namespace CBA.CORE.Models.ViewModels
{
    public class EditDetailRoleViewModel
    {
        public EditDetailRoleViewModel()
        {
            Users = new List<Claim>();
        }

        public string RoleName { get; set; }
        public State State { get; set; }
        public List<Claim> Users { get; set; }
    }
}
