using System;
using CBA.CORE.Models;
using CBA.Data;
using static CBA.CORE.Enums.Enums;

namespace CBA.DATA.Logic
{
    public class FinancialReportLogic
    {
        private readonly AppDbContext context;

        public FinancialReportLogic(AppDbContext context)
        {
            this.context = context;
        }

        public void CreateTransaction(GLAccount account, decimal amount, TransactionType trnType)
        {
            //Record this transaction for Trial Balance generation
            Transaction transaction = new Transaction();
            transaction.Amount = amount;
            transaction.Date = DateTime.Now;
            transaction.AccountName = account.AccountName;
            transaction.SubCategory = account.GLCategory.Name;
            transaction.MainCategory = account.GLCategory.MainGLCategory;
            transaction.TransactionType = trnType;

            context.Transactions.Add(transaction);
            context.SaveChanges();
        }

        public void CreateTransaction(CustomerAccount account, decimal amount, TransactionType trnType)
        {
            if (account.AccountType == AccountType.Loan)
            {
                //Record this transaction for Trial Balance generation
                Transaction transaction = new Transaction();
                transaction.Amount = amount;
                transaction.Date = DateTime.Now;
                transaction.AccountName = account.AccountName;
                transaction.SubCategory = "Customer's Loan Account";
                transaction.MainCategory = MainGLCategory.Asset;
                transaction.TransactionType = trnType;

                context.Transactions.Add(transaction);
                context.SaveChanges();
            }
            else
            {
                //Record this transaction for Trial Balance generation
                Transaction transaction = new Transaction();
                transaction.Amount = amount;
                transaction.Date = DateTime.Now;
                transaction.AccountName = account.AccountName;
                transaction.SubCategory = "Customer Account";
                transaction.MainCategory = MainGLCategory.Liability;
                transaction.TransactionType = trnType;

                context.Transactions.Add(transaction);
                context.SaveChanges();
            }
        }
    }
}
