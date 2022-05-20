using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBA.Core.Models;

namespace CBA.Data.Interfaces
{
    public interface ICustomerDao
    {
        //Customer Save(Customer item);
        Customer GetById(int id);
        //Customer Delete(long id);
        //Customer UpdateCustomer(Customer userChanges);
        IEnumerable<Customer> GetAll();
        //Customer GetRoles(Customer user);
        //Customer CheckStatus();
    }
}

