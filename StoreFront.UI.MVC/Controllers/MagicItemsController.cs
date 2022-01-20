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
    public class MagicItemsController : Controller
    {
        private StorefrontEntities db = new StorefrontEntities();

        // GET: MagicItems
        public ActionResult Index()
        {
            var magicItems = db.MagicItems.Include(m => m.Category).Include(m => m.Maker).Include(m => m.Rarity).Include(m => m.Status);
            return View(magicItems.ToList());
        }

        // GET: MagicItems/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MagicItem magicItem = db.MagicItems.Find(id);
            if (magicItem == null)
            {
                return HttpNotFound();
            }
            return View(magicItem);
        }

        // GET: MagicItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.MakerID = new SelectList(db.Makers, "MakerID", "FirstName");
            ViewBag.RarityID = new SelectList(db.Rarities, "RarityID", "RarityLevel");
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName");
            return View();
        }

        // POST: MagicItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MagicItemID,MagicItemName,Description,CategoryID,RarityID,Price,StatusID,MakerID,MagicItemImage")] MagicItem magicItem)
        {
            if (ModelState.IsValid)
            {
                db.MagicItems.Add(magicItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", magicItem.CategoryID);
            ViewBag.MakerID = new SelectList(db.Makers, "MakerID", "FirstName", magicItem.MakerID);
            ViewBag.RarityID = new SelectList(db.Rarities, "RarityID", "RarityLevel", magicItem.RarityID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", magicItem.StatusID);
            return View(magicItem);
        }

        // GET: MagicItems/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MagicItem magicItem = db.MagicItems.Find(id);
            if (magicItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", magicItem.CategoryID);
            ViewBag.MakerID = new SelectList(db.Makers, "MakerID", "FirstName", magicItem.MakerID);
            ViewBag.RarityID = new SelectList(db.Rarities, "RarityID", "RarityLevel", magicItem.RarityID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", magicItem.StatusID);
            return View(magicItem);
        }

        // POST: MagicItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MagicItemID,MagicItemName,Description,CategoryID,RarityID,Price,StatusID,MakerID,MagicItemImage")] MagicItem magicItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(magicItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", magicItem.CategoryID);
            ViewBag.MakerID = new SelectList(db.Makers, "MakerID", "FirstName", magicItem.MakerID);
            ViewBag.RarityID = new SelectList(db.Rarities, "RarityID", "RarityLevel", magicItem.RarityID);
            ViewBag.StatusID = new SelectList(db.Statuses, "StatusID", "StatusName", magicItem.StatusID);
            return View(magicItem);
        }

        // GET: MagicItems/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MagicItem magicItem = db.MagicItems.Find(id);
            if (magicItem == null)
            {
                return HttpNotFound();
            }
            return View(magicItem);
        }

        // POST: MagicItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            MagicItem magicItem = db.MagicItems.Find(id);
            db.MagicItems.Remove(magicItem);
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
