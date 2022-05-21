using System;
using CBA.Core.Models;
using CBA.CORE.Models;
using CBA.Data.Interfaces;
using CBA.DATA.Interfaces;
using static CBA.CORE.Enums.Enums;


namespace CBA.DATA.Logic
{
    public class CustomerAccountLogic
    {
        private readonly ICustomerDao customerDao;
        private readonly IAccountTypeManagementDao accountTypeManagementDao;

        public CustomerAccountLogic(ICustomerDao _customerDao, IAccountTypeManagementDao _accountTypeManagementDao)
        {
            customerDao = _customerDao;
            accountTypeManagementDao = _accountTypeManagementDao;
        }

        public string CreateAccountNumber(AccountType accountType, CustomerAccount customerAccount)
        {
            //int customerId = customerAccount.CustomerID;

            int consumerId = customerAccount.CustomerID;
            Customer consumer = customerDao.GetById(consumerId);

            if (String.IsNullOrWhiteSpace(consumer.CustomerLongID))
            {
                return "";
            }

            long longId = Convert.ToInt64(consumer.CustomerLongID);

            if (accountType == AccountType.Savings)
            {
                long accountNumber = AccountTypes.SavingsId + longId;
                return accountNumber.ToString();
            }

            if (accountType == AccountType.Current)
            {
                long accountNumber = AccountTypes.CurrentId + longId;
                return accountNumber.ToString();
            }
            if (accountType == AccountType.Loan)
            {
                long accountNumber = AccountTypes.LoanId + longId;
                return accountNumber.ToString();
            }

            return "";
        }

        public void ComputeFixedRepayment(CustomerAccount act, double nyears, double interestRate)
        {
            decimal totalAmountToRepay = 0;
            double nMonth = nyears * 12;
            double totalInterest = interestRate * nMonth * (double)act.LoanAmount;
            totalAmountToRepay = (decimal)totalInterest + (decimal)act.LoanAmount;
            act.LoanMonthlyRepay = (totalAmountToRepay / (12 * (decimal)nyears));
            act.LoanMonthlyPrincipalRepay = Convert.ToDecimal((double)act.LoanAmount / nMonth);
            act.LoanMonthlyInterestRepay = Convert.ToDecimal(totalInterest / nMonth);
            act.LoanPrincipalRemaining = (decimal)act.LoanAmount;
        }

        public void ComputeReducingRepayment(CustomerAccount act, double nyears, double interestRate)
        {
            double x = 1 - Math.Pow((1 + interestRate), -(nyears * 12));
            act.LoanMonthlyRepay = ((decimal)act.LoanAmount * (decimal)interestRate) / (decimal)x;

            act.LoanPrincipalRemaining = (decimal)act.LoanAmount;
            act.LoanMonthlyInterestRepay = (decimal)interestRate * act.LoanPrincipalRemaining;
            act.LoanMonthlyPrincipalRepay = act.LoanMonthlyRepay - act.LoanMonthlyInterestRepay;
        }

        public bool CheckIfAccountBalanceIsEnough(CustomerAccount account, decimal amountToDebit)
        {
            var accountConfig = accountTypeManagementDao.GetFirst();

            if (account.AccountType == AccountType.Savings)
            {
                if (account.AccountBalance >= amountToDebit + accountConfig.SavingsMinimumBalance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (account.AccountType == AccountType.Current)
            {

                if (account.AccountBalance >= amountToDebit + accountConfig.CurrentMinimumBalance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }


    public static class AccountTypes
    {
        public static long SavingsId = 10000000;
        public static long CurrentId = 20000000;
        public static long LoanId = 30000000;
    }
}
