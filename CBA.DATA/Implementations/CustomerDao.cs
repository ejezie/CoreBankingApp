using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.Data.Interfaces;
using System.Linq;

namespace CBA.Data.Implementations
{
    public class CustomerDao : ICustomerDao
    {
        private readonly AppDbContext context;

        public CustomerDao(AppDbContext context)
        {
            this.context = context;
        }

        public Customer Delete(long id)
        {
            Customer customer = context.Customers.Find(id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
            return customer;
        }

        public Customer RetrieveById(int id)
        {
            Customer customer = context.Customers.Find(id);
            return customer;
        }

        public Customer Save(Customer customer)
        {
            context.Customers.Add(customer);
            context.SaveChanges();
            return customer;
        }

        public Customer GetRoles(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Customer UpdateCustomer(Customer customerChanges)
        {
            var customer = context.Customers.Attach(customerChanges);
            customer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return customerChanges;
        }

        public Customer CheckStatus()
        {
            throw new NotImplementedException();
           
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = context.Customers.ToList();
            return customers;
        }
    }
}

