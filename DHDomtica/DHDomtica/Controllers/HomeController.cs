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

        private IEnumerable<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category {Id = 1, Name = "Stofzuigers"},
                new Category {Id = 2, Name = "Lampen"},
                new Category {Id = 3, Name = "Sletjes"},
                new Category {Id = 4, Name = "Tictacjes"},
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
                new Product {Id = 1, CatId = 1, Name = "Stofzuiger0", Summary ="adgfuiewfgueiwqgguewq", Price = 24.99, ImagePath = "http://placehold.it/700x400"},
                new Product {Id = 2, CatId = 1, Name = "Stofzuiger1", Summary ="adgfuiewfgueiwqgguewq", Price = 24.99, ImagePath = "http://placehold.it/700x400"},
                new Product {Id = 3, CatId = 1, Name = "Stofzuiger2", Summary ="adgfuiewfgueiwqgguewq", Price = 24.99, ImagePath = "http://placehold.it/700x400"},
                new Product {Id = 4, CatId = 1, Name = "Stofzuiger3", Summary ="adgfuiewfgueiwqgguewq", Price = 24.99, ImagePath = "http://placehold.it/700x400"},
                new Product {Id = 5, CatId = 2, Name = "Lamp0", Summary ="adgfuiewfgueiwqgguewq", Price = 24.99, ImagePath = "http://placehold.it/700x400"},
            };
        }


    }
}