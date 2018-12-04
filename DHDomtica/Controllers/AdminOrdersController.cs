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
            return View(orders);

        }
        public ActionResult Order()
        {
            return View();
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