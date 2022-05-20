using System;
using System.Collections.Generic;
using CBA.CORE.Models;
using static CBA.CORE.Enums.Enums;

namespace CBA.DATA.Interfaces
{
    public interface ICustomerAccountDao
    {
        List<CustomerAccount> GetByType(AccountType actType);
        int GetCountByCustomerActType(AccountType actType, int customerId);
        bool AnyAccountOfType(AccountType type);
    }
}
