using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;

namespace DHDomtica.Controllers
{
    public class StoreController : Controller
    {
        private DHDomoticaDBEntities db = new DHDomoticaDBEntities();

        // GET: Store
        public ActionResult Index()
        {
            
            return View();
        }

        //Code for the Store/ProductDetails
        // GET: ProductDetails
        public ActionResult ProductDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ShowSidebar();
            return View(product);
        }

        //Todo: Make this work
        public ActionResult Wishlist()
        {
            ViewBag.Message = "Yes, We are all wishing for a wishlist...";
            return View();
        }

        //Copied code from HomeController for the _sidebar
        private void ShowSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"Sidebar {Request.RawUrl}");
            ViewBag.ShowSideBar = true;
            ViewBag.AllCategories = db.MainCategory.ToList();
            ViewBag.AllProducts = db.Product.ToList();
        }

        //Generating ProductList by category ID
        public ActionResult Categories(int id)
        {
            ShowSidebar();
            var categorie = db.MainCategory.SingleOrDefault(c => c.ID == id);

            if (categorie == null)
                return HttpNotFound();

            var ProductList = new MainCategoryViewModel()
            {
                Category = categorie,
                Products = db.Product.Where(c => c.MainCategoryID.Equals(id)).ToList().AsEnumerable()

            };
            return View(ProductList);
        }

        public ActionResult ShoppingCart()
        {
            ViewBag.Message = "Welcome in the shoppingcart";

            return View();
        }
    }
}