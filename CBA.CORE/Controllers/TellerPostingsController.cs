using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CBA.Core.Models;
using CBA.CORE.Models;
using CBA.Data;
using CBA.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static CBA.CORE.Enums.Enums;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CBA.WebApi.Controllers
{
    public class TellerPostingsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IService service;
        private readonly UserManager<ApplicationUser> userManager;
        public TellerPostingsController(AppDbContext context, IService _service, UserManager<ApplicationUser> _userManager)
        {
            this.context = context;
            service = _service;
            userManager = _userManager;
        }
        // GET: TellerPostings
        public async Task<ActionResult> Index()
        {
            var tellerPostings = context.TellerPostings.Include(t => t.CustomerAccount).Include(t => t.TillAccount);
            return View(await tellerPostings.ToListAsync());
        }

        public async Task<string> GetLoggedInUserIdAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(userEmail);
            return user.Id;
        }

        // GET: TellerPostings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            TellerPosting tellerPosting = await context.TellerPostings.FindAsync(id);
            if (tellerPosting == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            return View(tellerPosting);
        }

        // GET: TellerPostings/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            CustomerAccount customerAccount = context.CustomerAccounts.Find(id);
            if (customerAccount == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }

            TellerPosting model = new TellerPosting();
            model.ConsumerAccountID = customerAccount.ID;

            ViewBag.PostingType = string.Empty;

            return View(model);

        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TellerPosting tellerPosting)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    // Get present user
                    string tellerId = await GetLoggedInUserIdAsync();

                    //check if the user has a till account to know if it is a teller

                    bool tellerHasTill = context.TillAccounts.Any(tu => tu.UserId.Equals(tellerId));
                    if (!tellerHasTill)
                    {
                        AddError("No till associated with logged in teller");
                        return View(tellerPosting);
                    }
                    //Get the till account ID of the current user
                    int tillId = context.TillAccounts.Where(tu => tu.UserId.Equals(tellerId)).First().GlAccountID;


                    tellerPosting.TillAccountID = tillId;
                    //Get the till account of the current user
                    var tillAct = context.GLAccounts.Find(tillId);


                    var custAct = context.CustomerAccounts.Find(tellerPosting.ConsumerAccountID);

                    tellerPosting.PostInitiatorId = tellerId;
                    tellerPosting.Date = DateTime.Now;

                    var amount = tellerPosting.Amount;
                    if (tellerPosting.PostingType == TellerPostingType.Withdrawal)
                    {
                        if (service.CheckIfAccountBalanceIsEnough(custAct, tellerPosting.Amount))
                        {
                            if (tillAct.AccountBalance <= tellerPosting.Amount)
                            {
                                AddError("Insuficient funds in till account");
                                return View(tellerPosting);
                            }
                        }
                        else
                        {
                            AddError("Insuficient funds in Customer account");
                            return View(tellerPosting);
                        }



                        tellerPosting.Status = PostStatus.Pending;
                        string result = service.PostTeller(custAct, tillAct, amount, TellerPostingType.Withdrawal);
                        if (!result.Equals("success"))
                        {
                            return RedirectToAction("TellerPostFail", new { message = result });
                        }

                        tellerPosting.Status = PostStatus.Approved;
                        context.TellerPostings.Add(tellerPosting);
                        await context.SaveChangesAsync();
                        return RedirectToAction("TellerPostSuccess");
                    }
                    else
                    {
                        //Teller Posting For Deposit
                        tellerPosting.Status = PostStatus.Pending;

                        {
                            //The teller is changed to Successful

                            string result =
                                service.PostTeller(custAct, tillAct, amount, TellerPostingType.Deposit);
                            if (!result.Equals("success"))
                            {
                                return RedirectToAction("TellerPostFail", new { message = result });
                            }

                            tellerPosting.Status = PostStatus.Approved;

                            //record transaction
                            service.CreateTransaction(tillAct, amount, TransactionType.Debit);
                            service.CreateTransaction(custAct, amount, TransactionType.Credit);
                        }
                        //db.SaveChanges();

                        context.TellerPostings.Add(tellerPosting);
                        context.SaveChanges();
                        return RedirectToAction("TellerPostSuccess");
                    }
                }
                catch
                {

                }
            }

            ViewBag.ConsumerAccountID = new SelectList(context.CustomerAccounts, "ID", "AccountName", tellerPosting.ConsumerAccountID);
            ViewBag.TillAccountID = new SelectList(context.GLAccounts, "ID", "AccountName", tellerPosting.TillAccountID);
            return View(tellerPosting);
        }

        // GET: TellerPostings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            TellerPosting tellerPosting = await context.TellerPostings.FindAsync(id);
            if (tellerPosting == null)
            {
                return RedirectToAction("ErrorView", "Error");
            }
            return View(tellerPosting);
        }

        // POST: TellerPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TellerPosting tellerPosting = await context.TellerPostings.FindAsync(id);
            context.TellerPostings.Remove(tellerPosting);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public ActionResult SelectAccount()
        {
            // check whether user has till here and bounce? or later in create's post ?

            return View(context.CustomerAccounts.Include(a => a.Branch).Where(a => a.AccountType != AccountType.Loan).ToList());
        }



        public async Task<ActionResult> UserPostsAsync()
        {
            string userId = await GetLoggedInUserIdAsync();
            var userTellerPostings = context.TellerPostings.Include(t => t.CustomerAccount).Where(t => t.PostInitiatorId.Equals(userId));
            //return View("Index", userTellerPostings.ToList());
            return View(userTellerPostings.ToList());
        }


        public ActionResult TellerPostFail()
        {
            return View();
        }

        public ActionResult TellerPostSuccess()
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
