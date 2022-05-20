using System;
using CBA.Core.Models;

namespace CBA.CORE.Models
{
    public class TellerTill
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int GlAccounID { get; set; }
        public virtual GLAccount GlAccount { get; set; }
    }
}
