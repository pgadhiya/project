using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using project.EDM;

namespace project.Areas.Techology.Controllers
{
    public class technologiesController : Controller
    {
        private technotaskEntities db = new technotaskEntities();

        // GET: Techology/technologies
        public ActionResult Index()
        {
            return View(db.tbltechnologies.ToList());
        }

        // GET: Techology/technologies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbltechnology tbltechnology = db.tbltechnologies.Find(id);
            if (tbltechnology == null)
            {
                return HttpNotFound();
            }
            return View(tbltechnology);
        }

        // GET: Techology/technologies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Techology/technologies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "T_ID,T_Name")] tbltechnology tbltechnology)
        {
            if (ModelState.IsValid)
            {
                db.tbltechnologies.Add(tbltechnology);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbltechnology);
        }

        // GET: Techology/technologies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbltechnology tbltechnology = db.tbltechnologies.Find(id);
            if (tbltechnology == null)
            {
                return HttpNotFound();
            }
            return View(tbltechnology);
        }

        // POST: Techology/technologies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "T_ID,T_Name")] tbltechnology tbltechnology)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbltechnology).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbltechnology);
        }

        // GET: Techology/technologies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbltechnology tbltechnology = db.tbltechnologies.Find(id);
            if (tbltechnology == null)
            {
                return HttpNotFound();
            }
            return View(tbltechnology);
        }

        // POST: Techology/technologies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbltechnology tbltechnology = db.tbltechnologies.Find(id);
            db.tbltechnologies.Remove(tbltechnology);
            db.SaveChanges();
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
