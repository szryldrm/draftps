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
    public class EducationTypesController : Controller
    {
        private DraftPSDbEntities db = new DraftPSDbEntities();

        // GET: EducationTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.EducationType.ToListAsync());
        }

        // GET: EducationTypes/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationType educationType = await db.EducationType.FindAsync(id);
            if (educationType == null)
            {
                return HttpNotFound();
            }
            return View(educationType);
        }

        // GET: EducationTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EducationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,DateTime,IsDeleted")] EducationType educationType)
        {
            if (ModelState.IsValid)
            {
                educationType.ID = Guid.NewGuid();
                db.EducationType.Add(educationType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(educationType);
        }

        // GET: EducationTypes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationType educationType = await db.EducationType.FindAsync(id);
            if (educationType == null)
            {
                return HttpNotFound();
            }
            return View(educationType);
        }

        // POST: EducationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,DateTime,IsDeleted")] EducationType educationType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(educationType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(educationType);
        }

        // GET: EducationTypes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationType educationType = await db.EducationType.FindAsync(id);
            if (educationType == null)
            {
                return HttpNotFound();
            }
            return View(educationType);
        }

        // POST: EducationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            EducationType educationType = await db.EducationType.FindAsync(id);
            db.EducationType.Remove(educationType);
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
