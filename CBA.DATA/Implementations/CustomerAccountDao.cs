using System;
using System.Collections.Generic;
using System.Linq;
using CBA.CORE.Enums;
using CBA.CORE.Models;
using CBA.Data;
using CBA.DATA.Interfaces;

namespace CBA.DATA.Implementations
{
    public class CustomerAccountDao : ICustomerAccountDao
    {
        private readonly AppDbContext context;

        public CustomerAccountDao (AppDbContext context)
        {
            this.context = context;
        }

        public bool AnyAccountOfType(Enums.AccountType type)
        {
            return context.CustomerAccounts.Any(a => a.AccountType == type);
        }

        public List<CustomerAccount> GetByType(Enums.AccountType actType)
        {
            return context.CustomerAccounts.Where(a => a.AccountType == actType).ToList();

        }

        public int GetCountByCustomerActType(Enums.AccountType actType, int customerId)
        {
            return context.CustomerAccounts.Where(a => a.AccountType == actType && a.Customer.ID == customerId).Count();

        }
    }
}
