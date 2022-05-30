using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.CORE.Models;
using CBA.Data;
using CBA.Data.Interfaces;
using CBA.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static CBA.CORE.Enums.Enums;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBA.WebApi.Controllers
{
    public class GlPostingsController : Controller
    {

        private readonly AppDbContext context;
        private readonly IService service;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        //private readonly ICustomerDao customerDao;

        public GlPostingsController(AppDbContext context, IService _service, ICustomerDao _customerDao, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            this.context = context;
            service = _service;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        public async Task<string> GetLoggedInUserIdAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(userEmail);
            return user.Id;
        }

        // GET: GlPostings
        public async Task<ActionResult> Index()
        {
            var glPostings = context.GlPostings.Include(g => g.CrGlAccount).Include(g => g.DrGlAccount);
            return View(await glPostings.ToListAsync());
        }

        public async Task<ActionResult> UserPostsAsync()
        {
            string userId = await GetLoggedInUserIdAsync();
            var userGlPostings = context.GlPostings.Include(g => g.CrGlAccount).Include(g => g.DrGlAccount).Where(g => g.PostInitiatorId.Equals(userId));
            //return View("Index", userGlPostings.ToList());
            return View(userGlPostings.ToList());
        }

        // GET: GlPostings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            GlPosting glPosting = await context.GlPostings.FindAsync(id);
            if (glPosting == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            return View(glPosting);
        }

        // GET: GlPostings/Create
        public ActionResult Create(int? crId, int? drId)
        {
            if (crId == null || drId == null || crId == drId)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            GLAccount drglAccount = context.GLAccounts.Find(drId);
            GLAccount crglAccount = context.GLAccounts.Find(crId);
            if (drglAccount == null || crglAccount == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }

            GlPosting model = new GlPosting();
            model.DrGlAccountID = drglAccount.ID;
            model.CrGlAccountID = crglAccount.ID;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( GlPosting glPosting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var drAct = context.GLAccounts.Find(glPosting.DrGlAccountID);
                    var crAct = context.GLAccounts.Find(glPosting.CrGlAccountID);

                    if (crAct != null)
                    {
                        if (crAct.AccountName.ToLower().Contains("till") ||
                            crAct.AccountName.ToLower().Contains("vault"))
                        {
                            if (crAct.AccountBalance < glPosting.CreditAmount)
                            {

                                AddError("Insufficient funds in asset account to be credited");
                                return View(glPosting);
                            }
                        }
                    }
                    else { AddError("credit account is read as null"); return View(glPosting); }

                    glPosting.Status = PostStatus.Pending;

                    decimal amt = glPosting.CreditAmount;

                    if (crAct.GLCategory.MainGLCategory == MainGLCategory.Asset || crAct.GLCategory.MainGLCategory == MainGLCategory.Expense)
                    {
                        if (crAct.AccountBalance < amt)
                        {
                            AddError("Insufficient funds in the asset or expense account to be credited");
                            return View(glPosting);
                        }
                    }

                    if (drAct.GLCategory.MainGLCategory == MainGLCategory.Capital ||
                        drAct.GLCategory.MainGLCategory == MainGLCategory.Liability ||
                        drAct.GLCategory.MainGLCategory == MainGLCategory.Income)
                    {
                        if (drAct.AccountBalance < amt)
                        {
                            AddError("Insufficient funds in the account to be debited");
                            return View(glPosting);
                        }
                    }
                    service.CreditGl(crAct, amt);
                    service.DebitGl(drAct, amt);

                    glPosting.Status = PostStatus.Approved;

                    glPosting.PostInitiatorId = User.Identity.Name;
                    glPosting.Date = DateTime.Now;

                    service.CreateTransaction(drAct, glPosting.DebitAmount, TransactionType.Debit);
                    service.CreateTransaction(crAct, glPosting.CreditAmount, TransactionType.Credit);

                    context.GlPostings.Add(glPosting);
                    context.SaveChanges();
                    return RedirectToAction("GlPostSuccess");
                }
                catch (Exception ex)
                {
                    AddError(ex.ToString());
                    return View(glPosting);
                }
            }


            AddError("Please, enter valid data");
            return View(glPosting);
        }

        // GET: GlPostings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            GlPosting glPosting = await context.GlPostings.FindAsync(id);
            if (glPosting == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            ViewBag.CrGlAccountID = new SelectList(context.GLAccounts, "ID", "AccountName", glPosting.CrGlAccountID);
            ViewBag.DrGlAccountID = new SelectList(context.GLAccounts, "ID", "AccountName", glPosting.DrGlAccountID);
            return View(glPosting);
        }

        // POST: GlPostings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GlPosting glPosting)
        {
            if (ModelState.IsValid)
            {
                context.Entry(glPosting).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CrGlAccountID = new SelectList(context.GLAccounts, "ID", "AccountName", glPosting.CrGlAccountID);
            ViewBag.DrGlAccountID = new SelectList(context.GLAccounts, "ID", "AccountName", glPosting.DrGlAccountID);
            return View(glPosting);
        }

        // GET: GlPostings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            GlPosting glPosting = await context.GlPostings.FindAsync(id);
            if (glPosting == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            return View(glPosting);
        }

        // POST: GlPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GlPosting glPosting = await context.GlPostings.FindAsync(id);
            context.GlPostings.Remove(glPosting);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public ActionResult SelectFirstAccount()
        {
            var glaccount = context.GLAccounts.Include(g => g.GLCategory).Include(g => g.GLCategory);
            return View(glaccount);
        }

        public ActionResult SelectSecondAccount(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            GLAccount crglAccount = context.GLAccounts.Find(id);
            if (crglAccount == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }


            ViewBag.CrGlAccountID = crglAccount.ID;
            var acts = context.GLAccounts.Where(g => g.ID != crglAccount.ID && g.GLCategory.MainGLCategory == crglAccount.GLCategory.MainGLCategory).ToList();
            return View(acts);
        }


        public ActionResult GlPostSuccess()
        {
            return View();
        }

        public ActionResult GlPostFail()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
        private void AddError(string error)
        {
            ModelState.AddModelError("", error);
        }
    }
}

