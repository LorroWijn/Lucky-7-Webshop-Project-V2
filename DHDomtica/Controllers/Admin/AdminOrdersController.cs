using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using System.Globalization;
using System.Runtime;
using System.Threading.Tasks;
using System.Text;
using System.Net.Mail;
using DHDomtica.ViewModels;

namespace DHDomtica.Controllers
{
    public class AdminOrdersController : Controller
    {
        // GET: AdminOrders
        private DHDomoticaDBEntities db = new DHDomoticaDBEntities();
        public ActionResult Index()
        {
            List<OrderProduct> orderProducts = db.OrderProducts.ToList();
            List<Order> orders = db.Orders.ToList();
            ShowAdminSidebar();
            return View(orders);

        }
        //Code for the AdminsideBar
        private void ShowAdminSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"AdminSidebar {Request.RawUrl}");
            ViewBag.ShowAdminSideBar = true;
        }

        public ActionResult Order(int OrderID)
            {
                Order order = db.Orders.FirstOrDefault(o => o.ID.Equals(OrderID));
                List<OrderProduct> OP = db.OrderProducts.Where(op => op.OrderID.Equals(order.ID)).ToList();
                List<ItemModel> OrderProducts = new List<ItemModel>();
                Session["Order"] = order;
                foreach (OrderProduct item in OP)
                {
                    ItemModel product = new ItemModel()
                    {
                        Product = db.Products.FirstOrDefault(p => p.ID.Equals(item.ProductID)),
                        Quantity = item.Quantity

                    };
                    OrderProducts.Add(product);
                }
                ShowAdminSidebar();
                return View(OrderProducts);

        }
        public ActionResult UpdateStatus(int Status, int ID)
        {
            string newStatus = "error";
            if (Status == 0)
            {
                newStatus = "Betaald";
            }
            else if(Status == 1)
            {
                newStatus = "Verzonden";
            }
            else if(Status == 2)
            {
                newStatus = "Bezorgd";
            }

                db.Orders.First(o => o.ID.Equals(ID)).OrderStatus = newStatus;
                db.SaveChanges();
            
            return RedirectToAction("Index", "AdminOrders");
        }
    }
}