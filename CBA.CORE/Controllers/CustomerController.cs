using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.Core.Models.ViewModels;
using CBA.Data;
using CBA.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBA.WebApi.Controllers
{
    public class CustomerController : Controller
    {
        // GET: /<controller>/
        private readonly ICustomerDao _customerdaoimplement;

        public CustomerController(ICustomerDao customerdaoimplement)
        {
            _customerdaoimplement = customerdaoimplement;
        }

        public IActionResult Index()
        {
            var customers = _customerdaoimplement.GetAllCustomers();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer newCustomer = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                };

                _customerdaoimplement.Save(newCustomer);
                //return RedirectToAction("index", new { id = newUser.Id });
                return RedirectToAction("index", "home", new { area = "" });
            }
           
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            DetailsCustomerViewModel detailsUserViewModel = new DetailsCustomerViewModel()
            {
                customer = _customerdaoimplement.RetrieveById(id),
                pageTitle = "Customer Details"
            };

            return View(detailsUserViewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _customerdaoimplement.RetrieveById(id);
            EditCustomerViewModel editUserViewModel = new EditCustomerViewModel()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Gender = customer.Gender
            };
            return View(editUserViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditCustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Customer customer = _customerdaoimplement.RetrieveById(model.Id);
                //Console.WriteLine(model.Id);

                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Gender = model.Gender;
               

                Customer updatedCustomer = _customerdaoimplement.UpdateCustomer(customer);

                return RedirectToAction("index", "home", new { area = "" });
            }

            return View(model);
        }
    }
}
