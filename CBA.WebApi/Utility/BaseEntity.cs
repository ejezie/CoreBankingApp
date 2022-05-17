using System;
using System.ComponentModel.DataAnnotations;

namespace CBA.Core.Utility
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public virtual DateTime DateCreated { get; set; }

        public virtual DateTime DateModified { get; set; }
    }
}