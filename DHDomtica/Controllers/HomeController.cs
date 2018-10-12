using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
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

            var categorie = GetCategories().SingleOrDefault(c => c.Id == id);

            if (categorie == null)
                return HttpNotFound();

            return View(categorie);
        }

        public ActionResult Products(int id)
        {
            ShowSidebar();

            var product = GetProducts().SingleOrDefault(p => p.Id == id);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        private IEnumerable<MainCategory> GetCategories()
        {
            return new List<MainCategory>
            {
                new MainCategory {Id = 1, Name = "Stofzuigers"},
                new MainCategory {Id = 2, Name = "Lampen"},
                new MainCategory {Id = 3, Name = "Koelkasten"},
                new MainCategory {Id = 4, Name = "Broodmachines"},
            };
        }

        private void ShowSidebar ()
        {
            ViewBag.ShowSideBar = true;
            ViewBag.AllCategories = GetCategories();
            ViewBag.AllProducts = GetProducts();
        }

        private IEnumerable<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product {Id = 1, Name = "Stofzuiger1", Description ="adgfuiewfgueiwqgguewq", Price = 24.99, ImagePath = "http://placehold.it/700x400", URL = "www.google.com", MainCatId = 1, SubCatId = 1},
                new Product {Id = 2, Name = "Stofzuiger2", Description ="Hewwo", Price = 24.99, ImagePath = "http://placehold.it/700x400", URL = "www.google.com", MainCatId = 1, SubCatId = 1},
                new Product {Id = 3, Name = "Stofzuiger3", Description ="Dit is iets", Price = 24.99, ImagePath = "http://placehold.it/700x400", URL = "www.google.com", MainCatId = 1, SubCatId = 2},
                new Product {Id = 4, Name = "Stofzuiger4", Description ="Awooo", Price = 24.99, ImagePath = "http://placehold.it/700x400", URL = "www.google.com", MainCatId = 1, SubCatId = 3},

                new Product {Id = 5, Name = "Lamp1", Description ="Dark light", Price = 24.99, ImagePath = "http://placehold.it/700x400", URL = "www.google.com", MainCatId = 2, SubCatId = 1},
                new Product {Id = 5, Name = "Lamp2", Description ="Oooh light", Price = 24.99, ImagePath = "http://placehold.it/700x400", URL = "www.google.com", MainCatId = 2, SubCatId = 1},

                new Product {Id = 5, Name = "Koelkast1", Description ="1 deurs koelkast", Price = 24.99, ImagePath = "http://placehold.it/700x400", URL = "www.google.com", MainCatId = 3, SubCatId = 1},
                new Product {Id = 5, Name = "Koelkast2", Description ="2 deurs koelkast", Price = 24.99, ImagePath = "http://placehold.it/700x400", URL = "www.google.com", MainCatId = 3, SubCatId = 2}
            };
        }


    }
}