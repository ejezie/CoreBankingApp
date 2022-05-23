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
//using Microsoft.EntityFrameworkCore;
//using CBA.CORE.Models;

//namespace CBA.WebApi.Controllers
//{
//    public class BranchesController : Controller
//    {
//        private readonly AppDbContext context;

//        public BranchesController(AppDbContext context)
//        {
//            this.context = context;
//        }

//        // GET: Branches
//        public async Task<ActionResult> Index()
//        {
//            return View(await context.Branches.ToListAsync());
//        }

//        // GET: Branches/Details/5
//        public async Task<ActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Branch branch = await context.Branches.FindAsync(id);
//            if (branch == null)
//            {
//                return HttpNotFound();
//            }
//            return View(branch);
//        }

//        // GET: Branches/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Address,SortCode")] Branch branch)
//        {
//            if (ModelState.IsValid)
//            {
//                context.Branches.Add(branch);
//                await context.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

//            return View(branch);
//        }

//        // GET: Branches/Edit/5
//        public async Task<ActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Branch branch = await context.Branches.FindAsync(id);
//            if (branch == null)
//            {
//                return HttpNotFound();
//            }
//            return View(branch);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address,SortCode")] Branch branch)
//        {
//            if (ModelState.IsValid)
//            {
//                context.Entry(branch).State = EntityState.Modified;
//                await context.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }
//            return View(branch);
//        }

//        public async Task<ActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Branch branch = await context.Branches.FindAsync(id);
//            if (branch == null)
//            {
//                return HttpNotFound();
//            }
//            return View(branch);
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<ActionResult> DeleteConfirmed(int id)
//        {
//            Branch branch = await context.Branches.FindAsync(id);
//            context.Branches.Remove(branch);
//            await context.SaveChangesAsync();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                context.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
