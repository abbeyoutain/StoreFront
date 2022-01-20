using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;

namespace StoreFront.UI.MVC.Controllers
{
    public class MakersController : Controller
    {
        private StorefrontEntities db = new StorefrontEntities();

        // GET: Makers
        public ActionResult Index()
        {
            return View(db.Makers.ToList());
        }

        // GET: Makers/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maker maker = db.Makers.Find(id);
            if (maker == null)
            {
                return HttpNotFound();
            }
            return View(maker);
        }

        // GET: Makers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Makers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MakerID,FirstName,LastName,Location")] Maker maker)
        {
            if (ModelState.IsValid)
            {
                db.Makers.Add(maker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maker);
        }

        // GET: Makers/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maker maker = db.Makers.Find(id);
            if (maker == null)
            {
                return HttpNotFound();
            }
            return View(maker);
        }

        // POST: Makers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MakerID,FirstName,LastName,Location")] Maker maker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maker);
        }

        // GET: Makers/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Maker maker = db.Makers.Find(id);
            if (maker == null)
            {
                return HttpNotFound();
            }
            return View(maker);
        }

        // POST: Makers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Maker maker = db.Makers.Find(id);
            db.Makers.Remove(maker);
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
