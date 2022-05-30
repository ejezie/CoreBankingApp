using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.Core.Models.ViewModels;
using CBA.Data;
using CBA.Data.Interfaces;
using CBA.Services.Interfaces;
//using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBA.WebApi.Controllers
{
    public class CustomerController : Controller
    {
        // GET: /<controller>/
        private readonly AppDbContext context;
        private readonly IService service;

        public CustomerController(IService _service, AppDbContext context)
        {
            this.context = context;
            service = _service;
        }

        public async Task<ActionResult> Index()
        {
            return View(await context.Customers.ToListAsync());
        }

        

        // GET: Consumers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "home");

            }
            Customer consumer = await context.Customers.FindAsync(id);
            if (consumer == null)
            {
                return RedirectToAction("index", "home");

            }
            return View(consumer);
        }

        public ActionResult Create(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CustomerLongID = service.GenerateCustomerLongId();

                if (customer.CustomerLongID != null && !String.IsNullOrWhiteSpace(customer.FullName))
                {
                    var customerInfo = (customer.FullName + " " + "(" + customer.CustomerLongID.ToString() + ")");
                    customer.CustomerInfo = customerInfo;
                }

                context.Customers.Add(customer);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Consumers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "home");

            }
            Customer consumer = await context.Customers.FindAsync(id);
            if (consumer == null)
            {
                return RedirectToAction("index", "home");

            }
            return View(consumer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Customer consumer)
        {
            if (ModelState.IsValid)
            {

                if (consumer.CustomerLongID != null && !String.IsNullOrWhiteSpace(consumer.FullName))
                {
                    var consumerInfo = (consumer.FullName + " " + "(" + consumer.CustomerLongID.ToString() + ")");
                    consumer.CustomerInfo = consumerInfo;
                }
                context.Entry(consumer).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(consumer);
        }

        // GET: Consumers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "home");

            }
            Customer consumer = await context.Customers.FindAsync(id);
            if (consumer == null)
            {
                return RedirectToAction("index", "home");

            }
            return View(consumer);
        }

        // POST: Consumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer consumer = await context.Customers.FindAsync(id);
            context.Customers.Remove(consumer);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
