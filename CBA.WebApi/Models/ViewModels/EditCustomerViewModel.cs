using System;
using System.ComponentModel.DataAnnotations;
using CBA.Core.Enums;

namespace CBA.Core.Models.ViewModels
{
    public class EditCustomerViewModel : AddCustomerViewModel
    {
        public int Id { get; set; }
    }
}
