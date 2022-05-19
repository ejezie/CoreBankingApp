using System;
namespace CBA.CORE.Enums
{
    public class Enums
    {
        public enum AccountCategory
        {
            Asset = 1,
            Liability = 2,
            Capital = 3,
            Income = 4,
            Expense = 5
        }

        public enum PostingType
        {
            Deposit = 1,
            Withdrawal = 2
        }

        public enum LoanStatus
        {
            Paid = 1,
            Unpaid = 2,
            Breeched = 3
        }

        public enum Business
        {
            Open = 1,
            Inbetween = 2,
            Closed = 3
        }
        public enum Status
        {
            INACTIVE = 0,
            ACTIVE = 1
        }
        public enum LoanStatusEnum
        {
            UNPAID = 1,
            PAID = 2,
            DEFAULTED = 3
        }
        public enum GenderEnum
        {
            MALE = 1,
            FEMALE = 2
        }
        public enum AccountStatus
        {
            Closed, Open
        }

        public enum TermsOfLoan
        {
            Fixed, Reducing
        }

        public enum AccountType
        {
            Savings, Current, Loan
        }
    }
}
