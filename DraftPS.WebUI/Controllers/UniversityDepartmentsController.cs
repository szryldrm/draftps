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
    public class UniversityDepartmentsController : Controller
    {
        private DraftPSDbEntities db = new DraftPSDbEntities();

        // GET: UniversityDepartments
        public async Task<ActionResult> Index()
        {
            var universityDepartment = db.UniversityDepartment.Include(u => u.Department).Include(u => u.University);
            return View(await universityDepartment.ToListAsync());
        }

        // GET: UniversityDepartments/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityDepartment universityDepartment = await db.UniversityDepartment.FindAsync(id);
            if (universityDepartment == null)
            {
                return HttpNotFound();
            }
            return View(universityDepartment);
        }

        // GET: UniversityDepartments/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Department, "ID", "Name");
            ViewBag.UniversityID = new SelectList(db.University, "ID", "Name");
            return View();
        }

        // POST: UniversityDepartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,UniversityID,DepartmentID,DateTime,IsDeleted")] UniversityDepartment universityDepartment)
        {
            if (ModelState.IsValid)
            {
                universityDepartment.ID = Guid.NewGuid();
                db.UniversityDepartment.Add(universityDepartment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Department, "ID", "Name", universityDepartment.DepartmentID);
            ViewBag.UniversityID = new SelectList(db.University, "ID", "Name", universityDepartment.UniversityID);
            return View(universityDepartment);
        }

        // GET: UniversityDepartments/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityDepartment universityDepartment = await db.UniversityDepartment.FindAsync(id);
            if (universityDepartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Department, "ID", "Name", universityDepartment.DepartmentID);
            ViewBag.UniversityID = new SelectList(db.University, "ID", "Name", universityDepartment.UniversityID);
            return View(universityDepartment);
        }

        // POST: UniversityDepartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,UniversityID,DepartmentID,DateTime,IsDeleted")] UniversityDepartment universityDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(universityDepartment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Department, "ID", "Name", universityDepartment.DepartmentID);
            ViewBag.UniversityID = new SelectList(db.University, "ID", "Name", universityDepartment.UniversityID);
            return View(universityDepartment);
        }

        // GET: UniversityDepartments/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniversityDepartment universityDepartment = await db.UniversityDepartment.FindAsync(id);
            if (universityDepartment == null)
            {
                return HttpNotFound();
            }
            return View(universityDepartment);
        }

        // POST: UniversityDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            UniversityDepartment universityDepartment = await db.UniversityDepartment.FindAsync(id);
            db.UniversityDepartment.Remove(universityDepartment);
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
