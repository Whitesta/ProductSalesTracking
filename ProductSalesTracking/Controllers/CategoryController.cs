using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductSalesTracking.Models.AdoEntity;

namespace ProductSalesTracking.Controllers
{
     
    public class CategoryController : Controller
    {
        ProductTrackingSystemEntities db = new ProductTrackingSystemEntities();
        // GET: Category
        public ActionResult Index()
        {
            var categories = db.TblCategory.ToList();
            return View(categories);
        }
        [HttpGet]
        public ActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCategory(TblCategory c)
        {
            db.TblCategory.Add(c);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var deleteCategory = db.TblCategory.Find(id);
            db.TblCategory.Remove(deleteCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CategoryGet(int id)
        {
            var category = db.TblCategory.Find(id);
            return View("CategoryGet", category);
        }
        public ActionResult CategoryUpdate(TblCategory c)
        {
            var updateCategory = db.TblCategory.Find(c.id);
           updateCategory.Name = c.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
    }
}