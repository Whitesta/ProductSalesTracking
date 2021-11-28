using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductSalesTracking.Models.AdoEntity;

namespace ProductSalesTracking.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        ProductTrackingSystemEntities db = new ProductTrackingSystemEntities();
        public ActionResult Index()
        {
            var sale = db.TblSales.ToList();
            return View(sale);
        }
        [HttpGet]
        public ActionResult NewSales()
        {
            // Get Product
            List<SelectListItem> product = (from x in db.TblProduct.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.Name,
                                             Value = x.id.ToString()
                                         }
                                     ).ToList();

            ViewBag.DropProduct = product;
             
            // Get Seller
            List<SelectListItem> seller = (from x in db.TblEmployee.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.Name+" "+x.SurName,
                                             Value = x.id.ToString()
                                         }
                                     ).ToList();

            ViewBag.DropSeller = seller;
            // Get Customer
            List<SelectListItem> customer = (from x in db.TblCustomer.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.Name+" "+x.SurName,
                                             Value = x.id.ToString()
                                         }
                                     ).ToList();

            ViewBag.DropCustomer = customer;
            return View();
        }
        [HttpPost]
        public ActionResult NewSales(TblSales sales)
        {
            var getProduct = db.TblProduct.Where(x => x.id == sales.TblProduct.id).FirstOrDefault();
            var getSeller = db.TblEmployee.Where(x => x.id == sales.TblEmployee.id).FirstOrDefault();
            var getCustomer = db.TblCustomer.Where(x => x.id == sales.TblCustomer.id).FirstOrDefault();
            sales.TblProduct = getProduct;
            sales.TblEmployee = getSeller;
            sales.TblCustomer = getCustomer;
            sales.Date = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.TblSales.Add(sales);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}