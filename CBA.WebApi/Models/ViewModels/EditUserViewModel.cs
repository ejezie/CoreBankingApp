using System;
namespace CBA.CORE.Models.ViewModels
{
    public class EditUserViewModel : AddUserViewModel
    {
        public string Id { get; set; }

        public bool IsEnabled { get; set; }
    }
}
