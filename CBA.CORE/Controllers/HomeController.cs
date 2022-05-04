using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBA.WebApi.Controllers
{
    public class HomeController : Controller
    //public 
    {
        private readonly ICustomerDao _customerdaoimplement;

        public HomeController(ICustomerDao implement)
        {
            _customerdaoimplement = implement;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
           var customers = _customerdaoimplement.GetAllCustomers();
            return View(customers);
        }
    }
}
