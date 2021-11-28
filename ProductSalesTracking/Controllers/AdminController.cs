using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductSalesTracking.Models.AdoEntity;

namespace ProductSalesTracking.Controllers
{
    public class AdminController : Controller
    {
        ProductTrackingSystemEntities db = new ProductTrackingSystemEntities();
        // GET: Admin
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NewAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewAdmin(TblUser u )
        {
            db.TblUser.Add(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}