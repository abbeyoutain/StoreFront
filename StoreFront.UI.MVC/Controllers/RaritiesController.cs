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
    public class RaritiesController : Controller
    {
        private StorefrontEntities db = new StorefrontEntities();

        // GET: Rarities
        public ActionResult Index()
        {
            return View(db.Rarities.ToList());
        }

        // GET: Rarities/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rarity rarity = db.Rarities.Find(id);
            if (rarity == null)
            {
                return HttpNotFound();
            }
            return View(rarity);
        }

        // GET: Rarities/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rarities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RarityID,RarityLevel")] Rarity rarity)
        {
            if (ModelState.IsValid)
            {
                db.Rarities.Add(rarity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rarity);
        }

        // GET: Rarities/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rarity rarity = db.Rarities.Find(id);
            if (rarity == null)
            {
                return HttpNotFound();
            }
            return View(rarity);
        }

        // POST: Rarities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RarityID,RarityLevel")] Rarity rarity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rarity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rarity);
        }

        // GET: Rarities/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rarity rarity = db.Rarities.Find(id);
            if (rarity == null)
            {
                return HttpNotFound();
            }
            return View(rarity);
        }

        // POST: Rarities/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            Rarity rarity = db.Rarities.Find(id);
            db.Rarities.Remove(rarity);
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
