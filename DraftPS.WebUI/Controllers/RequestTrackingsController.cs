using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DraftPS.WebUI.Models;

namespace DraftPS.WebUI.Controllers
{
    public class RequestTrackingsController : Controller
    {
        private DraftPSDbEntities db = new DraftPSDbEntities();

        // GET: RequestTrackings
        public async Task<ActionResult> Index()
        {
            return View(await db.RequestTracking.Where(s => s.IsDeleted == false).OrderBy(s => s.DateTime).ToListAsync());
        }

        // GET: RequestTrackings/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestTracking requestTracking = await db.RequestTracking.FindAsync(id);
            if (requestTracking == null)
            {
                return HttpNotFound();
            }
            return View(requestTracking);
        }

        // GET: RequestTrackings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestTrackings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,DateTime,IsDeleted")] RequestTracking requestTracking)
        {
            if (ModelState.IsValid)
            {
                requestTracking.ID = Guid.NewGuid();
                requestTracking.DateTime = DateTime.Now;
                requestTracking.IsDeleted = false;
                db.RequestTracking.Add(requestTracking);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(requestTracking);
        }

        // GET: RequestTrackings/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestTracking requestTracking = await db.RequestTracking.FindAsync(id);
            if (requestTracking == null)
            {
                return HttpNotFound();
            }
            return View(requestTracking);
        }

        // POST: RequestTrackings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,DateTime,IsDeleted")] RequestTracking requestTracking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestTracking).State = EntityState.Modified;
                requestTracking.DateTime = DateTime.Now;
                requestTracking.IsDeleted = false;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(requestTracking);
        }

        // GET: RequestTrackings/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestTracking requestTracking = await db.RequestTracking.FindAsync(id);
            if (requestTracking == null)
            {
                return HttpNotFound();
            }
            return View(requestTracking);
        }

        // POST: RequestTrackings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            RequestTracking requestTracking = await db.RequestTracking.FindAsync(id);
            requestTracking.IsDeleted = true;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
