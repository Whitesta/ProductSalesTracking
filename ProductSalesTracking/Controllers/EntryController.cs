using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductSalesTracking.Models.AdoEntity;
using System.Web.Security;

namespace ProductSalesTracking.Controllers
{
    public class EntryController : Controller
    {
        // GET: Entry
        ProductTrackingSystemEntities db = new ProductTrackingSystemEntities();
        public ActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Entry(TblUser us)
        {
            var result = db.TblUser.FirstOrDefault(x => x.user == us.user && x.password == us.password);
            if (result != null)
            {
                FormsAuthentication.SetAuthCookie(result.user, false);
                return RedirectToAction("Index", "Customers");
            }
            else
            {
                return View();
            }

            return View();
        }
    }
}