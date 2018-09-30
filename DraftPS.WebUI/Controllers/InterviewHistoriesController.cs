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
    public class InterviewHistoriesController : Controller
    {
        private DraftPSDbEntities db = new DraftPSDbEntities();

        // GET: InterviewHistories
        public async Task<ActionResult> Index()
        {
            var interviewHistory = db.InterviewHistory.Include(i => i.StudentRequest);
            return View(await interviewHistory.ToListAsync());
        }

        // GET: InterviewHistories/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewHistory interviewHistory = await db.InterviewHistory.FindAsync(id);
            if (interviewHistory == null)
            {
                return HttpNotFound();
            }
            return View(interviewHistory);
        }

        // GET: InterviewHistories/Create
        public ActionResult Create()
        {
            ViewBag.StudentRequestID = new SelectList(db.StudentRequest, "ID", "Name");
            return View();
        }

        // POST: InterviewHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,AdvisorID,StudentRequestID,Description,DateTime,IsDeleted")] InterviewHistory interviewHistory)
        {
            if (ModelState.IsValid)
            {
                interviewHistory.ID = Guid.NewGuid();
                db.InterviewHistory.Add(interviewHistory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StudentRequestID = new SelectList(db.StudentRequest, "ID", "Name", interviewHistory.StudentRequestID);
            return View(interviewHistory);
        }

        // GET: InterviewHistories/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewHistory interviewHistory = await db.InterviewHistory.FindAsync(id);
            if (interviewHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentRequestID = new SelectList(db.StudentRequest, "ID", "Name", interviewHistory.StudentRequestID);
            return View(interviewHistory);
        }

        // POST: InterviewHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,AdvisorID,StudentRequestID,Description,DateTime,IsDeleted")] InterviewHistory interviewHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewHistory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StudentRequestID = new SelectList(db.StudentRequest, "ID", "Name", interviewHistory.StudentRequestID);
            return View(interviewHistory);
        }

        // GET: InterviewHistories/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewHistory interviewHistory = await db.InterviewHistory.FindAsync(id);
            if (interviewHistory == null)
            {
                return HttpNotFound();
            }
            return View(interviewHistory);
        }

        // POST: InterviewHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            InterviewHistory interviewHistory = await db.InterviewHistory.FindAsync(id);
            db.InterviewHistory.Remove(interviewHistory);
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
