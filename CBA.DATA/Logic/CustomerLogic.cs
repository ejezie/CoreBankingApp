using System;
using System.Linq;
using CBA.Data.Interfaces;

namespace CBA.DATA.Logic
{
    public class CustomerLogic
    {
        private readonly ICustomerDao customerDao;

        public CustomerLogic(ICustomerDao _customerDao)
        {
            customerDao = _customerDao;
        }

        public string GenerateCustomerLongId()
        {

            string id = "00000001";

            var customers = customerDao.GetAll().OrderByDescending(c => c.ID);

            if (customers.Any())
            {
                long newId = Convert.ToInt64(customers.First().CustomerLongID);
                id = (newId + 1).ToString("D8");
            }

            return id;
        }
    }
}
