using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreFront.UI.MVC.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult Unresolved()
        {
            return View();
        }
    }
}