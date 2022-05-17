using System;
using System.Threading.Tasks;
using CBA.Core.Models;

namespace CBA.Services.Interfaces
{
    public interface IService
    {
        string GeneratePassword();
        string GenerateUserName(string firstname, string secondname);
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
