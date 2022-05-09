using System;
namespace CBA.Services.Interfaces
{
    public interface IService
    {
        string GeneratePassword();
        string GenerateUserName(string firstname, string secondname);
        object SendEmail(object sender, EventArgs e);
    }
}
