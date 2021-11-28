using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using ProductSalesTracking.Models.AdoEntity;

namespace ProductSalesTracking.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        ProductTrackingSystemEntities db = new ProductTrackingSystemEntities();
        [Authorize]
        public ActionResult Index(int page=1)
        {
            var customer = db.TblCustomer.Where(x=>x.State==true).ToList().ToPagedList(page, 5);
            //var customer = db.TblCustomer.ToList();
            return View(customer);
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(TblCustomer customer)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCustomer");
            }
            customer.State = true;
            db.TblCustomer.Add(customer);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult DeleteCustomer(TblCustomer customer)
        {
            var deleteCustomer = db.TblCustomer.Find(customer.id);
            deleteCustomer.State = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CustomerGet(int id)
        {
            var customer = db.TblCustomer.Find(id);
            return View("CustomerGet",customer);
        }

        public ActionResult CustomerUpdate(TblCustomer customer)
        {
         var updateCustomer = db.TblCustomer.Find(customer.id);
            updateCustomer.Name = customer.Name;
            updateCustomer.SurName = customer.SurName;
            updateCustomer.City = customer.City;
            updateCustomer.Balance = customer.Balance;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}