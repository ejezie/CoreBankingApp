using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.CORE.Models;
using static CBA.CORE.Enums.Enums;

namespace CBA.Services.Interfaces
{
    public interface IService
    {
        string GeneratePassword();
        string GenerateUserName(string firstname, string secondname);
        Task SendEmailAsync(MailRequest mailRequest);
        string CreateAccountNumber(AccountType accountType, CustomerAccount customerAccount);
        void ComputeFixedRepayment(CustomerAccount act, double nyears, double interestRate);
        void ComputeReducingRepayment(CustomerAccount act, double nyears, double interestRate);
        bool CheckIfAccountBalanceIsEnough(CustomerAccount account, decimal amountToDebit);
        string GenerateCustomerLongId();
        bool IsConfigurationSet();
        bool CreditCustomerAccount(CustomerAccount customerAccount, decimal amount);
        bool DebitCustomerAccount(CustomerAccount customerAccount, decimal amount);
        bool CreditGl(GLAccount account, decimal amount);
        bool DebitGl(GLAccount account, decimal amount);
        void CreateTransaction(GLAccount account, decimal amount, TransactionType trnType);
        void CreateTransaction(CustomerAccount account, decimal amount, TransactionType trnType);
        bool IsUniqueGLAccount(string glAccountName);
        long GenerateGLAccountNumber(MainGLCategory glMainCategory);
        bool IsUniqueGLAcategory(string glAccountName);
        long CreateGlCategoryCode(GLCategory glCategory);
        Task<List<ApplicationUser>> GetAllTellers();
    }
}
