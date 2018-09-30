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
    public class StudentRequestsController : Controller
    {
        private DraftPSDbEntities db = new DraftPSDbEntities();

        // GET: StudentRequests
        public async Task<ActionResult> Index()
        {
            var studentRequest = db.StudentRequest.Include(s => s.EducationType).Include(s => s.RequestTracking);
            return View(await studentRequest.ToListAsync());
        }

        // GET: StudentRequests/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRequest studentRequest = await db.StudentRequest.FindAsync(id);
            if (studentRequest == null)
            {
                return HttpNotFound();
            }
            return View(studentRequest);
        }

        // GET: StudentRequests/Create
        public ActionResult Create()
        {
            ViewBag.EducationTypeID = new SelectList(db.EducationType, "ID", "Name");
            ViewBag.RequestTrackingID = new SelectList(db.RequestTracking.OrderBy(s=>s.Name), "ID", "Name");
            return View();
        }

        // POST: StudentRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,AdvisorID,EducationTypeID,HomeCityID,RequestTrackingID,Name,Surname,DateTime,IsDeleted")] StudentRequest studentRequest)
        {
            if (ModelState.IsValid)
            {
                studentRequest.ID = Guid.NewGuid();
                db.StudentRequest.Add(studentRequest);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EducationTypeID = new SelectList(db.EducationType, "ID", "Name", studentRequest.EducationTypeID);
            ViewBag.RequestTrackingID = new SelectList(db.RequestTracking, "ID", "Name", studentRequest.RequestTrackingID);
            return View(studentRequest);
        }

        // GET: StudentRequests/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRequest studentRequest = await db.StudentRequest.FindAsync(id);
            if (studentRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.EducationTypeID = new SelectList(db.EducationType, "ID", "Name", studentRequest.EducationTypeID);
            ViewBag.RequestTrackingID = new SelectList(db.RequestTracking, "ID", "Name", studentRequest.RequestTrackingID);
            return View(studentRequest);
        }

        // POST: StudentRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,AdvisorID,EducationTypeID,HomeCityID,RequestTrackingID,Name,Surname,DateTime,IsDeleted")] StudentRequest studentRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentRequest).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EducationTypeID = new SelectList(db.EducationType, "ID", "Name", studentRequest.EducationTypeID);
            ViewBag.RequestTrackingID = new SelectList(db.RequestTracking, "ID", "Name", studentRequest.RequestTrackingID);
            return View(studentRequest);
        }

        // GET: StudentRequests/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRequest studentRequest = await db.StudentRequest.FindAsync(id);
            if (studentRequest == null)
            {
                return HttpNotFound();
            }
            return View(studentRequest);
        }

        // POST: StudentRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            StudentRequest studentRequest = await db.StudentRequest.FindAsync(id);
            db.StudentRequest.Remove(studentRequest);
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
