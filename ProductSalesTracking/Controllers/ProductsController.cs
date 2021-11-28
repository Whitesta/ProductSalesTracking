using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductSalesTracking.Models.AdoEntity;

namespace ProductSalesTracking.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        ProductTrackingSystemEntities db = new ProductTrackingSystemEntities();
        public ActionResult Index(string p)
        {
            var listProduct = from x in db.TblProduct select x;
            if (!string.IsNullOrEmpty(p))
            {
                listProduct = listProduct.Where(x => x.Name.Contains(p)&& x.State==true);
            }
           // var listProduct = db.TblProduct.Where(x=>x.State==true).ToList();
            return View(listProduct.ToList());
        }
        [HttpGet]
        public ActionResult NewProduct()
        {
            List<SelectListItem> ctgr = (from x in db.TblCategory.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.Name,
                                             Value = x.id.ToString()
                                         }
                                       ).ToList();

            ViewBag.Drop = ctgr;
            return View();
        }
        [HttpPost]
        public ActionResult NewProduct(TblProduct product)
        {
            var result = db.TblCategory.Where(x => x.id == product.TblCategory.id).FirstOrDefault();
            product.TblCategory = result;
            db.TblProduct.Add(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ProductGet(int id)
        {
            List<SelectListItem> ctgr = (from x in db.TblCategory.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.Name,
                                             Value = x.id.ToString()
                                         }
                                       ).ToList();

            var product = db.TblProduct.Find(id);
            ViewBag.Drop = ctgr;
            return View("ProductGet", product);
        }
        public ActionResult UpdateProduct(TblProduct product)
        {
            var updateProduct = db.TblProduct.Find(product.id);
            updateProduct.Name = product.Name;
            updateProduct.Brand = product.Brand;
            updateProduct.Stock = product.Stock;
            updateProduct.BuyPrice = product.BuyPrice;
            updateProduct.SellPrice = product.SellPrice;
            var assignCategoryId = db.TblCategory.Where(x => x.id == product.TblCategory.id).FirstOrDefault();
            updateProduct.Category = assignCategoryId.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProduct(TblProduct product)
        {
            var deleteProduct = db.TblProduct.Find(product.id);
            deleteProduct.State = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}