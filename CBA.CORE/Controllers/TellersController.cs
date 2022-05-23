using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using CBA.CORE.Models;
using CBA.CORE.Models.ViewModels;
using CBA.Data;
using CBA.DATA.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CBA.WebApi.Controllers
{
    public class TellersController : Controller
    {
        private readonly AppDbContext context;
        private readonly ITellerDao tellerDao;
        private readonly IGLAccountDao gLAccountDao;

        public TellersController(AppDbContext _context, ITellerDao _tellerDao, IGLAccountDao _gLAccountDao)
        {
            context = _context;
            tellerDao = _tellerDao;
            gLAccountDao = _gLAccountDao;
        }

        // GET: TillAccounts
        public async Task<ActionResult> Index()
        {
            var tellerDetails = await tellerDao.GetAllTellerDetails();
            var viewModel = new List<TillAccountViewModel>();

            foreach (var detail in tellerDetails)
            {
                TillAccountViewModel data;
                if (detail.GlAccountID == 0)
                {
                    data = new TillAccountViewModel
                    {
                        GLAccountName = "--",
                        AccountBalance = "--",
                        Username = context.Users.Find(detail.UserId).UserName,
                        HasDetails = false,
                        IsDeletable = false
                    };
                    viewModel.Add(data);
                }
                else
                {
                    var applicationUser = context.Users.Find(detail.UserId);
                    data = new TillAccountViewModel
                    {
                        GLAccountName = detail.GlAccount.AccountName,
                        Id = detail.Id,
                        Username = applicationUser.UserName,
                        AccountBalance = detail.GlAccount.AccountBalance.ToString(CultureInfo.InvariantCulture)
                    };
                    viewModel.Add(data);
                }
            }


            return View(viewModel);
        }

        // GET: TillAccounts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            TillAccount tillAccount = await context.TillAccounts.FindAsync(id);
            if (tillAccount == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            return View(tillAccount);
        }

        // GET: TillAccounts/Create
        public ActionResult Create()
        {
            var testList = new List<string> { "a", "b", "c" };

            ViewBag.Users = new SelectList(tellerDao.GetTellersWithNoTills().ToString(), "Id", "UserName");
            ViewBag.GlAccountID = new SelectList(gLAccountDao.GetTillsWithoutTellers(), "ID", "AccountName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TillAccount tillAccount)
        {
            if (ModelState.IsValid)
            {
                context.TillAccounts.Add(tillAccount);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Users = new SelectList((System.Collections.IEnumerable)tellerDao.GetTellersWithNoTills(), "Id", "UserName", tillAccount.UserId);
            ViewBag.GlAccountID = new SelectList((System.Collections.IEnumerable)tellerDao.GetTellersWithNoTills(), "ID", "AccountName", tillAccount.GlAccountID);
            return View(tillAccount);
        }

        // GET: TillAccounts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            TillAccount tillAccount = await context.TillAccounts.FindAsync(id);
            if (tillAccount == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            ViewBag.GlAccountID = new SelectList(context.GLAccounts, "ID", "AccountName", tillAccount.GlAccountID);
            return View(tillAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( TillAccount tillAccount)
        {
            if (ModelState.IsValid)
            {
                context.Entry(tillAccount).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GlAccountID = new SelectList(context.GLAccounts, "ID", "AccountName", tillAccount.GlAccountID);
            return View(tillAccount);
        }

        // GET: TillAccounts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            TillAccount tillAccount = await context.TillAccounts.FindAsync(id);
            if (tillAccount == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            return View(tillAccount);
        }

        // POST: TillAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TillAccount tillAccount = await context.TillAccounts.FindAsync(id);
            context.TillAccounts.Remove(tillAccount);
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
