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
    public class StudentUniversityRequestsController : Controller
    {
        private DraftPSDbEntities db = new DraftPSDbEntities();

        // GET: StudentUniversityRequests
        public async Task<ActionResult> Index()
        {
            var studentUniversityRequest = db.StudentUniversityRequest.Include(s => s.StudentRequest).Include(s => s.UniversityDepartment);
            return View(await studentUniversityRequest.ToListAsync());
        }

        // GET: StudentUniversityRequests/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentUniversityRequest studentUniversityRequest = await db.StudentUniversityRequest.FindAsync(id);
            if (studentUniversityRequest == null)
            {
                return HttpNotFound();
            }
            return View(studentUniversityRequest);
        }

        // GET: StudentUniversityRequests/Create
        public ActionResult Create()
        {
            ViewBag.StudentRequestID = new SelectList(db.StudentRequest, "ID", "Name");
            ViewBag.UniversityDepartmentID = new SelectList(db.UniversityDepartment, "ID", "ID");
            return View();
        }

        // POST: StudentUniversityRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,StudentRequestID,UniversityDepartmentID,DateTime,IsDeleted")] StudentUniversityRequest studentUniversityRequest)
        {
            if (ModelState.IsValid)
            {
                studentUniversityRequest.ID = Guid.NewGuid();
                db.StudentUniversityRequest.Add(studentUniversityRequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StudentRequestID = new SelectList(db.StudentRequest, "ID", "Name", studentUniversityRequest.StudentRequestID);
            ViewBag.UniversityDepartmentID = new SelectList(db.UniversityDepartment, "ID", "ID", studentUniversityRequest.UniversityDepartmentID);
            return View(studentUniversityRequest);
        }

        // GET: StudentUniversityRequests/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentUniversityRequest studentUniversityRequest = await db.StudentUniversityRequest.FindAsync(id);
            if (studentUniversityRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentRequestID = new SelectList(db.StudentRequest, "ID", "Name", studentUniversityRequest.StudentRequestID);
            ViewBag.UniversityDepartmentID = new SelectList(db.UniversityDepartment, "ID", "ID", studentUniversityRequest.UniversityDepartmentID);
            return View(studentUniversityRequest);
        }

        // POST: StudentUniversityRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,StudentRequestID,UniversityDepartmentID,DateTime,IsDeleted")] StudentUniversityRequest studentUniversityRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentUniversityRequest).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StudentRequestID = new SelectList(db.StudentRequest, "ID", "Name", studentUniversityRequest.StudentRequestID);
            ViewBag.UniversityDepartmentID = new SelectList(db.UniversityDepartment, "ID", "ID", studentUniversityRequest.UniversityDepartmentID);
            return View(studentUniversityRequest);
        }

        // GET: StudentUniversityRequests/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentUniversityRequest studentUniversityRequest = await db.StudentUniversityRequest.FindAsync(id);
            if (studentUniversityRequest == null)
            {
                return HttpNotFound();
            }
            return View(studentUniversityRequest);
        }

        // POST: StudentUniversityRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            StudentUniversityRequest studentUniversityRequest = await db.StudentUniversityRequest.FindAsync(id);
            db.StudentUniversityRequest.Remove(studentUniversityRequest);
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
