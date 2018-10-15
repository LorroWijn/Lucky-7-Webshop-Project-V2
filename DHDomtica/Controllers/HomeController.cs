using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using DHDomtica.Models;

namespace DHDomtica.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ShowSidebar();
            return View();
        }

        public ActionResult About()
        {
            ShowSidebar();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ShowSidebar();
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShoppingCart()
        {
            ViewBag.Message = "Welcome in the shoppingcart";

            return View();
        }

        public ActionResult InlogPage()
        {
            return View();
        }

        public ActionResult Categories(int id)
        {
            ShowSidebar();

            var categorie = GetCategories().SingleOrDefault(c => c.ID == id);

            if (categorie == null)
                return HttpNotFound();

            return View(categorie);
        }

        public ActionResult Product(int ID)
        {
            ShowSidebar();

            var product = GetProducts().SingleOrDefault(p => p.ID == ID);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        private IEnumerable<MainCategory> GetCategories()
        {
            return new List<MainCategory>
            {
/*Fix this with a working DB*/
                new MainCategory {ID = 1, Name = "Stofzuigers"},
                new MainCategory {ID = 2, Name = "Lampen"},
                new MainCategory {ID = 3, Name = "Koelkasten"},
                new MainCategory {ID = 4, Name = "Broodmachines"},
            };
        }

        private void ShowSidebar()
        {
            ViewBag.ShowSideBar = true;
            ViewBag.AllCategories = GetCategories();
            ViewBag.AllProducts = GetProducts();
        }

        private IEnumerable<Product> GetProducts()
        {
            return new List<Product>

            ///*Fix this with a working DB*/
            {
                new Product { ID = 1, ProductNaam = "Stofzuiger1", ProductDescription = "adgfuiewfgueiwqgguewq", ProductPrice = 24, ProductImage = "http://placehold.it/700x400", URL = "www.google.com", Maincategory = "System", Subcategory = "Klokhuis" },
                new Product { ID = 2, ProductNaam = "Stofzuiger2", ProductDescription = "Hewwo", ProductPrice = 24, ProductImage = "http://placehold.it/700x400", URL = "www.google.com", Maincategory = "System", Subcategory = "Klokhuis" },
                new Product { ID = 3, ProductNaam = "Stofzuiger3", ProductDescription = "Dit is iets", ProductPrice = 24, ProductImage = "http://placehold.it/700x400", URL = "www.google.com", Maincategory = "System", Subcategory = "Klokhuis" },
                new Product { ID = 4, ProductNaam = "Stofzuiger4", ProductDescription = "Awooo", ProductPrice = 24, ProductImage = "http://placehold.it/700x400", URL = "www.google.com", Maincategory = "System", Subcategory = "Klokhuis" },

            };
        }



        public ActionResult Search(string searchString)
        {

            ViewBag.Message = "wel type domme kut";
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.Message = "Geen overeenkomende zoekresultaten op: " + searchString;
            }
            return View();
        }

    }
}