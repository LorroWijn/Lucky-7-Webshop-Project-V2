﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DHDomtica.Controllers
{
    public class HomeController : Controller
    {
        public static bool LoggedIn;
        public static string HelloWorld;

        //Begin database connection
        private DHDomoticaDBEntities _context;

        public HomeController()
        {
            _context = new DHDomoticaDBEntities();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //End of Database connection
        public ActionResult Index()
        {
            
            if (System.Web.HttpContext.Current.Request.Cookies["UserEMail"] != null)
            {
                HttpCookie NewCookie = Request.Cookies["UserName"];
                HelloWorld = NewCookie.Value;
                LoggedIn = true;
                ShowSidebar();
                return View();
            }
            else
            {
                LoggedIn = false;
                ShowSidebar();
                return View();
            }

        }

        public ActionResult BlogDom()
        {
            return View();
        }

        public ActionResult BlogAlexa()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress("DHDomotica@outlook.com"));  // replace with valid value 
                message.From = new MailAddress("DHDomotica@outlook.com");  // replace with valid value
                message.Subject = "Your email subject";
                message.Body = string.Format(body, model.FromName, model.FromEmail, model.Message);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "DHDomotica@outlook.com",  // replace with valid value
                        Password = "DHDadmin"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(model);
        }

        public ActionResult Sent()
        {
            return View();
        }

        private void ShowSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"Sidebar {Request.RawUrl}");
            ViewBag.ShowSideBar = true;
            ViewBag.AllCategories = _context.MainCategory.ToList();
            ViewBag.AllProducts = _context.Product.ToList();
        }

        public ActionResult Search(int selectedValue, string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                ViewBag.Message = "Er is geen zoekopdracht ingevoerd. Hier zijn al onze producten.";
            }
            var category = _context.MainCategory.FirstOrDefault();
            int cid = selectedValue;


            var ProductList = new MainCategoryViewModel();
            if (cid == 0)
            {
                ProductList = new MainCategoryViewModel()
                {
                    Category = category,
                    Products = _context.Product
                            .Where(c => c.Name.Contains(searchString) || c.Description.Contains(searchString))
                            .ToList().AsEnumerable()

                };
            }
            else
            {
                ProductList = new MainCategoryViewModel()
                {
                    Category = category,
                    Products = _context.Product
                            .Where(c => (c.Name.Contains(searchString) || c.Description.Contains(searchString)) && c.MainCategoryID.Equals(cid))
                            .ToList().AsEnumerable()

                };
            }
            if (ProductList.Products.ToList().Count == 0)
            {
                ViewBag.Message = "Geen overeenkomende zoekresultaten op: " + searchString;
            }
            return View(ProductList);

        }
    }
}