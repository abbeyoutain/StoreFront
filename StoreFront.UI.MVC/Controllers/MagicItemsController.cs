using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing; //added for Image
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using StoreFront.UI.MVC.Models;
using StoreFront.UI.MVC.Utilities; //added for access to ImageUtility class

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

        //Shopping Cart Step 3
        public ActionResult AddToCart(int qty, int @magicItemID)
        {
            //Create an empty shell for a local shopping cart variable.
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            //Check if session-cart exists; if so, that means there were already items in the shopping cart and we need 
            //to put those pre-existing items in the local shoppingCart collection we created above.
            if (Session["cart"] != null)
            {
                //Session cart exists - put its items in the local shopping cart collection so that they are easier to work with
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
                //This is unboxing. Session object gets cast back to its original, more specific type. This is explicit casting.
            }
            else
            {
                //if session cart doesn't exist yet, we need to instantiate it.
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            //find the product the user is trying to add to the cart
            MagicItem product = db.MagicItems.Where(x => x.MagicItemID == magicItemID).FirstOrDefault();

            if (product == null)
            {
                //if a bad ID was passed to this method, kick the user back to some page to try again or we could throw an error.
                //(this is the best method we know now, later this will be a good oppurtunity for custom error handling)
                return RedirectToAction("Index");
            }
            else
            {
                //if book id IS valid, add the line-item to the cart
                CartItemViewModel item = new CartItemViewModel(qty, product);

                //put item in the local shoppingCart collection. BUT if we already have that product as a cart-item, then we will update the qty only
                if (shoppingCart.ContainsKey(product.MagicItemID))
                {
                    shoppingCart[product.MagicItemID].Qty += qty;
                }
                else
                {
                    shoppingCart.Add(product.MagicItemID, item);
                }

                //now update the Session version of the cart so we can maintain that info between Request and Response cycles
                Session["cart"] = shoppingCart; //implicit casting aka boxing
            }

            //send them to a view that shows the list of all items in the cart
            return RedirectToAction("Index", "ShoppingCart");

        }

        // GET: MagicItems/Create
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MagicItemID,MagicItemName,Description,CategoryID,RarityID,Price,StatusID,MakerID,MagicItemImage")] MagicItem magicItem, HttpPostedFileBase itemImage)
        {
            if (ModelState.IsValid)
            {

                //Image Upload utility step 6
                #region File Upload
                string file = "NoImage.png";

                if (itemImage != null)
                {
                    //Process the file that was uploaded by the user
                    file = itemImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    //This if checks to see if the file uploaded is the right type of file
                    //i.e. file extension would be included in the goodExts
                    //We will also require the file uploaded to be 4 MB or less in size
                    if (goodExts.Contains(ext.ToLower()) && itemImage.ContentLength <= 4194304)
                    {
                        //create a new file name using a GUID (Globally Unique Identifier)
                        file = Guid.NewGuid() + ext;

                        string savePath = Server.MapPath("~/Content/imgstore/books/"); //this is where the images will be saved
                        Image convertedImage = Image.FromStream(itemImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                    }
                }
                //no matter what, update the PhotoUrl with the value of the file variable
                magicItem.MagicItemImage = file;
                #endregion

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MagicItemID,MagicItemName,Description,CategoryID,RarityID,Price,StatusID,MakerID,MagicItemImage")] MagicItem magicItem, HttpPostedFileBase itemImage)
        {
            if (ModelState.IsValid)
            {
                //Image Upload utility step 10
                #region File Upload
                if (itemImage != null)
                {
                    //get file name
                    string file = itemImage.FileName;

                    //get the file extension
                    string ext = file.Substring(file.LastIndexOf('.'));

                    //create a list of good extensions
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && itemImage.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;
                        string savePath = Server.MapPath("~/Content/images/");
                        Image convertedImage = Image.FromStream(itemImage.InputStream);
                        int maxImageSize = 500;
                        int maxThumbSize = 100;

                        ImageUtility.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);

                        if (magicItem.MagicItemImage != null && magicItem.MagicItemImage != "NoImage.png")
                        {
                            string path = Server.MapPath("~/Content/images/");
                            ImageUtility.Delete(path, magicItem.MagicItemImage);
                        }

                        magicItem.MagicItemImage = file; //this updates the book object before it is saved to the DB with the latest file name
                    }
                }
                #endregion

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
