using System;
using CBA.CORE.Models;
using CBA.DATA.Interfaces;
using static CBA.CORE.Enums.Enums;

namespace CBA.DATA.Logic
{
    public class DebitAndCreditLogic
    {
        private readonly IAccountTypeManagementDao accountTypeManagementDao;

        public DebitAndCreditLogic(IAccountTypeManagementDao _accountTypeManagementDao)
        {
            accountTypeManagementDao = _accountTypeManagementDao;
        }

        public bool IsConfigurationSet()
        {
            var config = accountTypeManagementDao.GetFirst();
            if (config.SavingsInterestExpenseGl == null || config.SavingsInterestPayableGl == null || config.CurrentInterestExpenseGl == null || config.COTIncomeGl == null || config.LoanInterestIncomeGl == null || config.LoanInterestReceivableGl == null)
            { 
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CreditConsumerAccount(CustomerAccount customerAccount, decimal amount)
        {

            try
            {
                if (customerAccount.AccountType == AccountType.Loan)
                {
                    customerAccount.AccountBalance -= amount;    //Because a Loan Account is an Asset Account
                }
                else
                {
                    customerAccount.AccountBalance += amount;     //Because a Savings or Current Account is a Liability Account
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DebitConsumerAccount(CustomerAccount customerAccount, decimal amount)
        {

            try
            {
                if (customerAccount.AccountType == AccountType.Loan)
                {
                    customerAccount.AccountBalance += amount;    //Because a Loan Account is an Asset Account
                }
                else
                {
                    customerAccount.AccountBalance -= amount;     //Because a Savings or Current Account is a Liability Account
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreditGl(GLAccount account, decimal amount)
        {
            try
            {
                switch (account.GLCategory.MainGLCategory)
                {
                    case MainGLCategory.Asset:
                        account.AccountBalance -= amount;
                        break;
                    case MainGLCategory.Capital:
                        account.AccountBalance += amount;
                        break;
                    case MainGLCategory.Expense:
                        account.AccountBalance -= amount;
                        break;
                    case MainGLCategory.Income:
                        account.AccountBalance += amount;
                        break;
                    case MainGLCategory.Liability:
                        account.AccountBalance += amount;
                        break;
                    default:
                        break;
                }//end switch

                //frLogic.CreateTransaction(account, amount, TransactionType.Credit);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }//end creditGl

        public bool DebitGl(GLAccount account, decimal amount)
        {
            try
            {
                switch (account.GLCategory.MainGLCategory)
                {
                    case MainGLCategory.Asset:
                        account.AccountBalance += amount;
                        break;
                    case MainGLCategory.Capital:
                        account.AccountBalance -= amount;
                        break;
                    case MainGLCategory.Expense:
                        account.AccountBalance += amount;
                        break;
                    case MainGLCategory.Income:
                        account.AccountBalance -= amount;
                        break;
                    case MainGLCategory.Liability:
                        account.AccountBalance -= amount;
                        break;
                    default:
                        break;
                }//end switch
                //frLogic.CreateTransaction(account, amount, TransactionType.Debit);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }//end DebitGl
    }
}
