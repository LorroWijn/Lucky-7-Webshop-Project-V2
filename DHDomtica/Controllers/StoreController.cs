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
            //read cookie from request
            HttpCookie pageCookie = Request.Cookies["pageCookie"];
            if (pageCookie == null)
            {
                //no cookie found or it is expired (30 min)
            }

            //cookie is found, check if the cookie has the value as expected
            if (!string.IsNullOrEmpty(pageCookie.Values["pageId"]))
            {
                //put the cookie in a viewbag
                ViewBag.pageId = pageCookie.Values["pageId"].ToString();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ShowSidebar();
            return View(product);

        }
        public ActionResult AddToCart(int product)
        {

            if (Session["cart"] == null)
            {
                List<int> products = new List<int>();

                products.Add(product);
                Session["cart"] = products;
                ViewBag.cart = products.Count();
                

                Session["count"] = 1;


            }
            else
            {
                List<int> products = (List<int>)Session["cart"];
                var contains = products.Where(c => c.Equals(product));
                
                    if(!products.Contains(product))
                {
                    products.Add(product);
                    Session["cart"] = products;
                    ViewBag.cart = products.Count();
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                    contains = null;
                }
            }

            return RedirectToAction("ShoppingCart", "Store");


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
            ViewBag.AllCategories = db.MainCategories.ToList();
            ViewBag.AllProducts = db.Products.ToList();
        }

        //Generating ProductList by category ID
        public ActionResult Categories(int id)
        {
            ShowSidebar();
            var categorie = db.MainCategories.SingleOrDefault(c => c.ID == id);

            if (categorie == null)
                return HttpNotFound();

            var ProductList = new MainCategoryViewModel()
            {
                Category = categorie,
                Products = db.Products.Where(c => c.MainCategoryID.Equals(id)).ToList().AsEnumerable()

            };
            return View(ProductList);
        }

        //Generating ProductList by category ID
        public ActionResult Products(int id)
        {
            ShowSidebar();

            return View(db.Set<Product>());
        }

        //pagination
        public ActionResult Pagination(int categoryId, int pageId)
        {
            ShowSidebar();
            //create a cookie
            HttpCookie pageCookie = new HttpCookie("pageCookie");
            //add key-values in the cookie
            pageCookie.Values.Add("pageId", pageId.ToString());
            pageCookie.Expires = DateTime.Now.AddMinutes(30);
            //write the cookie to the client
            Response.Cookies.Add(pageCookie);

            ViewBag.PageId = pageId;
            var categorie = db.MainCategories.SingleOrDefault(c => c.ID == categoryId);

            if (categorie == null)
                return HttpNotFound();

            int items = 12;
            int skipPages = 0;
            int maxProducts = db.Products.Where(c => c.MainCategoryID.Equals(categoryId)).Count();
            ViewBag.maxPages = maxProducts / items;
            if (pageId > 1)
            {
                skipPages = items * pageId;
            }


            var ProductList = new MainCategoryViewModel()
            {
                Category = categorie,
                Products = db.Products.Where(c => c.MainCategoryID.Equals(categoryId)).ToList().AsEnumerable().Skip(skipPages).Take(items)

            };

            return View(ProductList);
        }

        public ActionResult ShoppingCart()
        {
            
            var products = db.Products.Where(p => p.Name.Equals("feyenoord"));
            IEnumerable<Product> ProductList = products.AsEnumerable();
          
            List<int> IDS = (List<int>)Session["cart"];
            if (IDS != null)
            {
                foreach (int i in IDS)
                {   
                    
                    products = db.Products.Where(p => p.ID.Equals(i));
                    ProductList = ProductList.Concat(products.AsEnumerable());
                    
                }
            }         
                return View(ProductList.ToList());


        }
    }
}