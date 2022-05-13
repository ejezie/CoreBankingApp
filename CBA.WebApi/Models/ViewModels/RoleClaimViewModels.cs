using System.Collections.Generic;
//using CBA.DATA;

namespace CBA.CORE.Models.ViewModels

{
    public class RoleClaimsViewModel
    {
        public RoleClaimsViewModel()
        {
            Cliams = new List<RoleClaim>();
        }
        public string Id { get; set; }
        public List<RoleClaim> Cliams { get; set; }
    }
}