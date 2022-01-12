using PagedList;
using System;
using System.Collections.Generic;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanDienThoai.Models;
using Gremlin.Net.Process.Traversal;
using Order = WebBanDienThoai.Models.Order;

namespace WebBanDienThoai.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        private WebMobileEntities db = new WebMobileEntities();
        public ActionResult Index(string q)
        {
           

            decimal sum = 0;
            var count_order = (from or in db.Orders select or.orderID).Count();
            ViewBag.count_product = count_order;
            var model = db.Orders.Where(x => x.Status == 3).ToList();
            if (!string.IsNullOrEmpty(q))
            {
                int a = Convert.ToInt32(q);
                model = model.Where(x => x.CreatedAt.Value.Month == a).ToList();
                foreach(var iteam in model)
                {
                    sum += iteam.TotalMoney;
                
                }
                ViewBag.TotalMoney = sum;
         }

            var printorder = from i in db.Configures select new ConfigModel { Logo = i.Logo, Address_NameCompany = i.Address, Hotline = i.Hotline, Email_config = i.Email, NameCompany = i.NameCompany };
            ViewBag.printorder = printorder.ToList();
            ViewBag.keyword_search = q;
            return View(model);
        }
       
        public ActionResult Doanhthu(string q)
        {
            decimal sum = 0;
            decimal sumIn = 0;
            var count_order = (from or in db.Orders select or.orderID).Count();
            ViewBag.count_product = count_order;
            var model = db.Orders.Where(x => x.Status == 3).ToList();
            if (!string.IsNullOrEmpty(q))
            {
                int a = Convert.ToInt32(q);
                model = model.Where(x => x.CreatedAt.Value.Month == a).ToList();
                foreach (var iteam in model)
                {

                    sum += iteam.TotalMoney;
                    var model2 = db.OrderDetails.Where(x => x.orderID == iteam.orderID).ToList();
                    foreach(var iteam2 in model2)
                    {
                        var model3 = db.Products.Where(x => x.ProductID == iteam2.ProductID).SingleOrDefault();
                        sumIn +=(decimal) model3.PriceIn;
                    }    


                }
                decimal DoanhThuOut = sum - sumIn;

                ViewBag.TotalMoney = sum;
                ViewBag.TotalMoney1 = sumIn;
                ViewBag.TotalMoney2 = DoanhThuOut;
            }

            var printorder = from i in db.Configures select new ConfigModel { Logo = i.Logo, Address_NameCompany = i.Address, Hotline = i.Hotline, Email_config = i.Email, NameCompany = i.NameCompany };
            ViewBag.printorder = printorder.ToList();
            ViewBag.keyword_search = q;
            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult Details(int? id)
        {

            Order order = db.Orders.Find(id);

            var listOrder = from g in db.Products
                            join p in db.OrderDetails on g.ProductID equals p.ProductID
                            where p.orderID == id
                            select new OrderModel { ProductID = g.ProductID, ProductName = g.ProductName, Price = g.Price, Images = g.Images, Quanlity = p.Quanlity };
            ViewBag.order_item = listOrder;




            var printorder = from i in db.Configures select new ConfigModel { Logo = i.Logo, Address_NameCompany = i.Address, Hotline = i.Hotline, Email_config = i.Email, NameCompany = i.NameCompany };
            ViewBag.printorder = printorder.ToList();



            order.ViewStatus = true;

            var count_order = (from or in db.Orders where or.ViewStatus == false select or.orderID).Count();

            if (count_order > 0)
            {
                Session["countnewcart"] = count_order;
            }

            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            Customer cus = db.Customers.Find(order.customerID);
            ViewBag.Cus = new Customer()
            {
                customerID = cus.customerID,
                Address = cus.Address,
                customerName = cus.customerName,
                Phone = cus.Phone,
                Email = cus.Email
            };
            return View(order);
        }



    }
}