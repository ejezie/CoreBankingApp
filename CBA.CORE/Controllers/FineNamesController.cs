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
//    public class FineNamesController : Controller
//    {
//        private readonly AppDbContext context;

//        public FineNamesController(AppDbContext context)
//        {
//            this.context = context;
//        }

//            // GET: FineNames
//            public async Task<ActionResult> Index()
//            {
//                return View(await context.FineNames.ToListAsync());
//            }

//            // GET: FineNames/Details/5
//            public async Task<ActionResult> Details(int? id)
//            {
//                if (id == null)
//                {
//                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//                }
//                FineNames fineNames = await context.FineNames.FindAsync(id);
//                if (fineNames == null)
//                {
//                    return HttpNotFound();
//                }
//                return View(fineNames);
//            }

//            // GET: FineNames/Create
//            public ActionResult Create()
//            {
//                return View();
//            }

//            [HttpPost]
//            [ValidateAntiForgeryToken]
//            public async Task<ActionResult> Create([Bind(Include = "Id,Name")] FineNames fineNames)
//            {
//                if (ModelState.IsValid)
//                {
//                    context.FineNames.Add(fineNames);
//                    await context.SaveChangesAsync();
//                    return RedirectToAction("Index");
//                }

//                return View(fineNames);
//            }

//            // GET: FineNames/Edit/5
//            public async Task<ActionResult> Edit(int? id)
//            {
//                if (id == null)
//                {
//                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//                }
//                FineNames fineNames = await context.FineNames.FindAsync(id);
//                if (fineNames == null)
//                {
//                    return HttpNotFound();
//                }
//                return View(fineNames);
//            }

//            // POST: FineNames/Edit/5
//            // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//            // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//            [HttpPost]
//            [ValidateAntiForgeryToken]
//            public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] FineNames fineNames)
//            {
//                if (ModelState.IsValid)
//                {
//                    context.Entry(fineNames).State = EntityState.Modified;
//                    await context.SaveChangesAsync();
//                    return RedirectToAction("Index");
//                }
//                return View(fineNames);
//            }

//            // GET: FineNames/Delete/5
//            public async Task<ActionResult> Delete(int? id)
//            {
//                if (id == null)
//                {
//                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//                }
//                FineNames fineNames = await context.FineNames.FindAsync(id);
//                if (fineNames == null)
//                {
//                    return HttpNotFound();
//                }
//                return View(fineNames);
//            }

//            // POST: FineNames/Delete/5
//            [HttpPost, ActionName("Delete")]
//            [ValidateAntiForgeryToken]
//            public async Task<ActionResult> DeleteConfirmed(int id)
//            {
//                FineNames fineNames = await context.FineNames.FindAsync(id);
//                context.FineNames.Remove(fineNames);
//                await context.SaveChangesAsync();
//                return RedirectToAction("Index");
//            }

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
