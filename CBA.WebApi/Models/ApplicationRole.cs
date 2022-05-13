using System;
using CBA.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace CBA.CORE.Models
{
    public class ApplicationRole : IdentityRole
    {
        public State State { get; set; } = State.Enabled;
    }
}
