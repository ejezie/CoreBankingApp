using System;
using CBA.Core.Utility;

namespace CBA.CORE.Models
{
    public class GLCategory : BaseEntity
    {
        public virtual string Name { get; set; }
        public virtual MainAccount MainCategory { get; set; }
        public virtual string Description { get; set; }
    }
}
