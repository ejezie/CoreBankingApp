using System;
using System.Security.Cryptography;
using CBA.Services.Interfaces;
using System.Net.Mail;

namespace CBA.Services.Implementations
{
    public class Service : IService
    {
        public string GeneratePassword()
        {
            using RNGCryptoServiceProvider cryptRNGen = new();
            byte[] tokenBuffer = new byte[12];
            cryptRNGen.GetBytes(tokenBuffer);
            return Convert.ToBase64String(tokenBuffer);
        }

        public string GenerateUserName(string firstname, string secondname)
        {
            //Store the second name attach substring of first name.
            var possibleUsername = string.Format("{0}_{1}", secondname, firstname.Substring(0, 1));
            return possibleUsername;
        }

        public object SendEmail(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
