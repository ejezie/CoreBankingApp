using System;
using CBA.Core.Enums;

namespace CBA.CORE.Models.ViewModels
{
    public class EditUserViewModel : AddUserViewModel
    {
        public string Id { get; set; }

        public UserState UserState { get; set; }
    }
}
