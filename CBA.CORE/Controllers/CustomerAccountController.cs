//using System;
//using System.Collections.Generic;
//using System.Data;
////using System.Data.Entity;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using CBA.Data;
//using CBA.Services.Interfaces;
//using CBA.Data.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using CBA.CORE.Models;
//using static CBA.CORE.Enums.Enums;

//namespace CBA.WebApi.Controllers
//{
//    public class CustomerAccountController : Controller
//    {
//        private readonly AppDbContext context;
//        private readonly IService service;
//        private readonly ICustomerDao customerDao;

//        public CustomerAccountController(AppDbContext context, IService _service, ICustomerDao _customerDao)
//        {
//            this.context = context;
//            service = _service;
//            customerDao = _customerDao;
//        }



//        // GET: ConsumerAccounts
//        public async Task<ActionResult> Index()
//        {
//            var consumerAccounts = context.CustomerAccounts.Include(c => c.Branch).Include(c => c.Customer).Include(c => c.LinkedAccount);
//            return View(await consumerAccounts.ToListAsync());
//        }

//        // GET: ConsumerAccounts/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            CustomerAccount customerAccount = await context.CustomerAccounts.FindAsync(id);
//            if (customerAccount == null)
//            {
//                return HttpNotFound();
//            }
//            return View(customerAccount);
//        }

//        // GET: ConsumerAccounts/Create
//        public ActionResult Create()
//        {
//            ViewBag.BranchID = new SelectList(context.Branches, "ID", "Name");
//            ViewBag.ConsumerID = new SelectList(context.Customers, "ID", "ConsumerInfo");
//            ViewBag.LinkedAccountID = new SelectList(context.CustomerAccounts, "ID", "AccountName");
//            return View();
//        }

    
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "ID,AccountName,AccountNumber,AccountBalance,BranchID,AccountStatus,AccountType,ConsumerID,LoanMonthlyInterestRepay,LoanMonthlyRepay,LoanMonthlyPrincipalRepay,LoanPrincipalRemaining,TermsOfLoan,LoanAmount,LinkedAccountID")] CustomerAccount consumerAccount)
//        {
//            if (ModelState.IsValid)
//            {
//                if (consumerAccount.AccountType == AccountType.Savings ||
//                    consumerAccount.AccountType == AccountType.Current)
//                {
//                    consumerAccount.AccountBalance = 0;
//                    consumerAccount.AccountNumber =
//                        //_customerAccountLogic.CreateAccountNumber(customerAccount, customerAccount.AccountType);
//                        service.CreateAccountNumber(consumerAccount.AccountType, consumerAccount);
                   
//                    context.CustomerAccounts.Add(consumerAccount);
//                    await context.SaveChangesAsync();
//                    return RedirectToAction("Index");
//                }

//                return RedirectToAction("CreateLoan");

//            }

//            var savcurList = new List<AccountType> { AccountType.Savings, AccountType.Current };

//            ViewBag.BranchID = new SelectList(context.Branches, "ID", "Name", consumerAccount.BranchID);
//            ViewBag.ConsumerID = new SelectList(context.Customers, "ID", "ConsumerInfo", consumerAccount.CustomerID);
//            ViewBag.LinkedAccountID = new SelectList(context.CustomerAccounts, "ID", "AccountName", consumerAccount.LinkedAccountID);
//            return RedirectToAction("CreateLoan");
//        }

//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            CustomerAccount consumerAccount = await context.CustomerAccounts.FindAsync(id);
//            if (consumerAccount == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.BranchID = new SelectList(context.Branches, "ID", "Name", consumerAccount.BranchID);
//            ViewBag.ConsumerID = new SelectList(context.Customers, "ID", "ConsumerInfo", consumerAccount.CustomerID);
//            ViewBag.LinkedAccountID = new SelectList(context.CustomerAccounts, "ID", "AccountName", consumerAccount.LinkedAccountID);
//            return View(consumerAccount);
//        }

    
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "ID,AccountName,AccountNumber,AccountBalance,BranchID,AccountStatus,AccountType,ConsumerID,LoanMonthlyInterestRepay,LoanMonthlyRepay,LoanMonthlyPrincipalRepay,LoanPrincipalRemaining,TermsOfLoan,LoanAmount,LinkedAccountID,LoanInterestRatePerMonth,DaysCount,dailyInterestAccrued,SavingsWithdrawalCount,CurrentLien")] CustomerAccount consumerAccount)
//        {
//            if (ModelState.IsValid)
//            {
//                context.Entry(consumerAccount).State = EntityState.Modified;
//                await context.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            ViewBag.BranchID = new SelectList(context.Branches, "ID", "Name", consumerAccount.BranchID);
//            ViewBag.ConsumerID = new SelectList(context.Customers, "ID", "ConsumerInfo", consumerAccount.CustomerID);
//            ViewBag.LinkedAccountID = new SelectList(context.CustomerAccounts, "ID", "AccountName", consumerAccount.LinkedAccountID);
//            return View(consumerAccount);
//        }

//        // GET: ConsumerAccounts/Delete/5
//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            CustomerAccount consumerAccount = await context.CustomerAccounts.FindAsync(id);
//            if (consumerAccount == null)
//            {
//                return HttpNotFound();
//            }
//            return View(consumerAccount);
//        }

//        // POST: ConsumerAccounts/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            CustomerAccount consumerAccount = await context.CustomerAccounts.FindAsync(id);
//            context.CustomerAccounts.Remove(consumerAccount);
//            await context.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

//        public ActionResult CreateLoan()
//        {
//            ViewBag.BranchID = new SelectList(context.Branches, "ID", "Name");
//            ViewBag.ConsumerID = new SelectList(context.Customers, "ID", "ConsumerInfo");
//            ViewBag.LinkedAccountID = new SelectList(context.Customers, "ID", "AccountNumber");
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> CreateLoan([Bind(Include = "ID,AccountName,AccountNumber,AccountBalance,BranchID,AccountStatus,AccountType,ConsumerID,LoanMonthlyInterestRepay,LoanMonthlyRepay,LoanMonthlyPrincipalRepay,LoanPrincipalRemaining,TermsOfLoan,LoanAmount,LinkedAccountID")] CustomerAccount customerAccount)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    customerAccount.AccountBalance = 0;
//                    customerAccount.AccountType = AccountType.Loan;
//                    customerAccount.AccountNumber =
//                        //_customerAccountLogic.CreateAccountNumber(customerAccount, customerAccount.AccountType);
//                        service.CreateAccountNumber(AccountType.Loan, customerAccount);


//                    var linkedID = customerAccount.LinkedAccountID.GetValueOrDefault();
//                    CustomerAccount linkedConsumerAccount = null;
//                    if (linkedID != 0)
//                    {
//                        linkedConsumerAccount =
//                            context.CustomerAccounts.Where(c => c.ID == linkedID).SingleOrDefault();
//                    }

//                    if (linkedConsumerAccount == null)
//                    {
//                        ReturnView("servicing account number does not exist", customerAccount);
//                        return View(customerAccount);
//                    }
//                    // check if servicing account number actually belongs to customer and is either savings or current.
//                    if (linkedConsumerAccount.AccountType == AccountType.Loan || linkedConsumerAccount.CustomerID != customerAccount.CustomerID)
//                    {
//                        ReturnView("Invalid Linked Account", customerAccount);
//                        return View(customerAccount);
//                    }

//                    if (linkedConsumerAccount.AccountStatus == AccountStatus.Closed)
//                    {
//                        ReturnView("Linked Account is Closed", customerAccount);
//                        return View(customerAccount);
//                    }

//                    linkedID = linkedID;

//                    customerAccount.LoanInterestRatePerMonth = Convert.ToDecimal(2);

//                    switch (customerAccount.TermsOfLoan)
//                    {
//                        case TermsOfLoan.Fixed:
//                            service.ComputeFixedRepayment(customerAccount, 1, 2);
//                            break;
//                        case TermsOfLoan.Reducing:
//                            service.ComputeReducingRepayment(customerAccount, 1, 2);
//                            break;
//                        default:
//                            break;
//                    }
//                    // loan disbursement
//                    service.DebitCustomerAccount(customerAccount, customerAccount.LoanAmount);
//                    service.CreditCustomerAccount(linkedConsumerAccount, customerAccount.LoanAmount);

//                    context.CustomerAccounts.Add(customerAccount);
//                    await context.SaveChangesAsync();
//                    return RedirectToAction("Index");
//                }
//                catch (Exception ex)
//                {
//                    ReturnView(ex.ToString(), customerAccount);
//                    return View(customerAccount);
//                }

//            }

//            ReturnView("enter valid data", customerAccount);
//            return View(customerAccount);
//        }





//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                context.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private void AddError(string error)
//        {
//            ModelState.AddModelError("", error);
//        }

//        private void ReturnView(string error, CustomerAccount consumerAccount)
//        {
//            ViewBag.BranchID = new SelectList(context.Branches, "ID", "Name", consumerAccount.BranchID);
//            ViewBag.ConsumerID = new SelectList(context.Customers, "ID", "ConsumerInfo", consumerAccount.CustomerID);
//            ViewBag.LinkedAccountID = new SelectList(context.CustomerAccounts, "ID", "AccountNumber", consumerAccount.LinkedAccountID);
//            AddError(error);

//        }
//    }
//}
