using System;
using System.Collections.Generic;
using System.Linq;
using CBA.CORE.Models;
using CBA.DATA.Interfaces;
using static CBA.CORE.Enums.Enums;

namespace CBA.DATA.Implementations
{
    public class BalanceSheetDao : IBalanceSheetDao
    {
        private readonly IGLAccountDao gLAccountDao;
        private readonly ICustomerAccountDao customerAccountDao;
        public BalanceSheetDao(IGLAccountDao _gLAccountDao, ICustomerAccountDao _customerAccountDao)
        {
            gLAccountDao = _gLAccountDao;
            customerAccountDao = _customerAccountDao;
        }

        public List<GLAccount> GetAssetAccounts()
        {
            var allAssets = gLAccountDao.GetByMainCategory(MainGLCategory.Asset);

            GLAccount loanAsset = new GLAccount();
            loanAsset.AccountName = "Loan Accounts";
            var loanAccounts = customerAccountDao.GetByType(AccountType.Loan);
            foreach (var act in loanAccounts)
            {
                loanAsset.AccountBalance += act.AccountBalance;
            }
            allAssets.Add(loanAsset);
            return allAssets;
        }

        public List<GLAccount> GetCapitalAccounts()
        {
            var allCapitals = gLAccountDao.GetByMainCategory(MainGLCategory.Capital);
            //adding the "Reserves" capitals--> Profit or loss expressed as (Income - Expense)
            GLAccount reserveCapital = new GLAccount();
            reserveCapital.AccountName = "Reserves";
            decimal incomeSum = gLAccountDao.GetByMainCategory(MainGLCategory.Income).Sum(a => a.AccountBalance);
            decimal expenseSum = gLAccountDao.GetByMainCategory(MainGLCategory.Expense).Sum(a => a.AccountBalance);
            reserveCapital.AccountBalance = incomeSum - expenseSum;
            allCapitals.Add(reserveCapital);

            return allCapitals;
        }

        public List<LiabilityViewModel> GetLiabilityAccounts()
        {
            var liability = gLAccountDao.GetByMainCategory(MainGLCategory.Liability);

            var allLiabilityAccounts = new List<LiabilityViewModel>();

            foreach (var account in liability)
            {
                var model = new LiabilityViewModel();
                model.AccountName = account.AccountName;
                model.Amount = account.AccountBalance;

                allLiabilityAccounts.Add(model);

            }
            //adding customer's savings and loan accounts since they are liabilities to the bank           
            var savingsAccounts = customerAccountDao.GetByType(AccountType.Savings);
            var savingsLiability = new LiabilityViewModel();
            savingsLiability.AccountName = "Savings Accounts";
            savingsLiability.Amount = savingsAccounts != null ? savingsAccounts.Sum(a => a.AccountBalance) : 0;

            var currentAccounts = customerAccountDao.GetByType(AccountType.Current);
            var currentLiability = new LiabilityViewModel();
            currentLiability.AccountName = "Current Accounts";
            currentLiability.Amount = currentAccounts != null ? currentAccounts.Sum(a => a.AccountBalance) : 0;

            allLiabilityAccounts.Add(savingsLiability);
            allLiabilityAccounts.Add(currentLiability);
            return allLiabilityAccounts;
        }//end method
    
    }
}
