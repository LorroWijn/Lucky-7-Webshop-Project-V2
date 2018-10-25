using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Data.Linq.Mapping;
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

            var categorie = _context.MainCategory.SingleOrDefault(c => c.ID == id);

            if (categorie == null)
                return HttpNotFound();
            var ProductList = new MainCategoryViewModel()
            {
                Category = categorie,
                Products = _context.Product.ToList().AsEnumerable()

            };
            return View(ProductList);
        }

        public ActionResult Products(int id)
        {
            ShowSidebar();

            var product = GetProducts().SingleOrDefault(p => p.ID == id);

            if (product == null)
                return HttpNotFound();

            return View(product);
        }


        private void ShowSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"Sidebar {Request.RawUrl}");
            ViewBag.ShowSideBar = true;
            ViewBag.AllCategories = _context.MainCategory.ToList();
            ViewBag.AllProducts = GetProducts();
        }

        private IEnumerable<Product> GetProducts()
        {

            using (var db = new MyDBContext())
            {

                /*var MainCategorySelected = "Smart Home";*/

                /*var QuerydProducts = (from Product in db.Products
                                      where Product.Maincategory == MainCategorySelected
                                      select Product);*/

                //IQueryable<object> q = QuerydProducts;
                //List<object> l = new List<object>(q);
/*

                return QuerydProducts.ToList();*/
                return new List<Product>();
            }
        }

        public ActionResult Search(string searchString)
        {

            ViewBag.Message = "Er is geen zoekopdracht ingevoer.";
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.Message = "Geen overeenkomende zoekresultaten op: " + searchString;
            }
            return View();
        }

    }
}