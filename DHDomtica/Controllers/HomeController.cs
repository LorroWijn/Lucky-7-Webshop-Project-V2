using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;


namespace DHDomtica.Controllers
{
    public class HomeController : Controller
    {
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
            ShowSidebar();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

       public ActionResult ShoppingCart()
        {
            ViewBag.Message = "Welcome in the shoppingcart";

            return View();
        }

        private void ShowSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"Sidebar {Request.RawUrl}");
            ViewBag.ShowSideBar = true;
            ViewBag.AllCategories = _context.MainCategory.ToList();
            ViewBag.AllProducts = _context.Product.ToList();
        }

        public ActionResult Search(string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                ViewBag.Message = "Er is geen zoekopdracht ingevoerd. Hier zijn al onze producten.";
            }
            //else{
                int cid = 4;
                var categorie = _context.MainCategory.SingleOrDefault(c => c.ID == cid);

                if (categorie == null)
                    return HttpNotFound();

                var ProductList = new MainCategoryViewModel()
                {
                    Category = categorie,
                    Products = _context.Product.Where(c => c.Name.Contains(searchString)).ToList().AsEnumerable()

                };

                if (ProductList.Products.ToList().Count == 0)
                {
                    ViewBag.Message = "Geen overeenkomende zoekresultaten op: " + searchString;
                }
            return View(ProductList);
            //}
        }
    }
}