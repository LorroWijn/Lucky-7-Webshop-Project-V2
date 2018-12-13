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
using PayPal.Api;
using System.Globalization;
using System.Runtime;
using System.Threading.Tasks;
using System.Text;
using System.Net.Mail;
using DHDomtica.ViewModels;

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
        public ActionResult ProductDetails(int id)
        {
            
            //read cookie from request
            HttpCookie pageCookie = Request.Cookies["pageCookie"];
            if (pageCookie == null)
            {
                //no cookie found or it is expired (30 min)
            }
            else
            {
                //cookie is found, check if the cookie has the value as expected
                if (!string.IsNullOrEmpty(pageCookie.Values["pageId"]))
                {
                    //put the cookie in a viewbag
                    ViewBag.pageId = pageCookie.Values["pageId"].ToString();
                }
            }

            var product = db.Products.SingleOrDefault(p => p.ID == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var ProductReviewsList = new ProductReviewsViewModel()
            {
                
                Product = product,
                Reviews = db.Reviews.Where(c => c.ProductID.Equals(id)).ToList().AsEnumerable()

            };

            //average star calculation for in productDetails
            /*int totalReviewsCount = db.Reviews.Where(t => t.ProductID.Equals(id)).Count();
            int allStars = db.Reviews.Where(s => s.Stars.Equals(id)).Count();

            int averageStars = allStars / totalReviewsCount;*/

            ShowSidebar();
            return View(ProductReviewsList);

        }

        public ActionResult AddToWishlist(int product)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["UserEMail"] != null)
            {
                Wishlist wishlist = new Wishlist();
                wishlist.ProductID = product;
                wishlist.UserID = Convert.ToInt32(Request.Cookies["UserID"].Value);
                List<Wishlist> wl = db.Wishlists.Where(w => w.UserID.Equals(wishlist.UserID)).ToList();
                bool New = true;
                foreach (Wishlist l in wl)
                {
                    if (l.ProductID == wishlist.ProductID)
                    {
                        New = false;
                    }
                }
                if (New)
                {
                    db.Wishlists.Add(wishlist);
                    db.SaveChanges();
                }
            }


            return RedirectToAction("Wishlist", "Store");
        }
        public ActionResult RemoveFromWish(int product)
        {
            Wishlist wishlist = new Wishlist();
            int UserID = Convert.ToInt32(Request.Cookies["UserID"].Value);
            wishlist = db.Wishlists.First(w =>
                w.ProductID.Equals(product) &&
                w.UserID.Equals(UserID));
            db.Wishlists.Remove(wishlist);
            db.SaveChanges();
            return RedirectToAction("Wishlist", "Store");
        }
        //Todo: Make this work
        public ActionResult Wishlist()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["UserEMail"] != null)
            {
                List<Wishlist> wishlist = db.Wishlists.ToList();
                List<Product> WP = new List<Product>();
                int userid = Convert.ToInt32(Request.Cookies["UserID"].Value);
                List<int> products = new List<int>();
                foreach (Wishlist w in wishlist)
                {
                    if (w.UserID == userid)
                    {
                        products.Add(w.ProductID);
                    }
                }
                foreach (int p in products)
                {
                    WP.Add(db.Products.First(x => x.ID.Equals(p)));
                }

                return View(WP.ToList());
            }
            ViewBag.Message = "U moet ingelogd zijn om producten in uw wishlist te kunnen zetten";
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
        public ActionResult AddToCart(int product)
        {
            if (Session["cart"] == null)
            {
                CreateCart(product);
            }
            else
            {
                List<ItemModel> products = (List<ItemModel>)Session["cart"];
                int index = Find(product);
                if (index != -1)
                {
                    AddOne(product, products, index);
                }
                else

                {
                    AddNew(product, products, index);
                }
                Session["cart"] = products;
            }

            return RedirectToAction("ShoppingCart", "Store");
        }
        public void CreateCart(int product)
        {
            List<ItemModel> products = new List<ItemModel>();

            products.Add(new ItemModel { Product = db.Products.FirstOrDefault(p => p.ID.Equals(product)), Quantity = 1 });
            Session["cart"] = products;
            ViewBag.cart = products.Count();
            Session["count"] = 1;
        }
        public void AddNew(int product, List<ItemModel> products, int index)
        {
            products.Add(new ItemModel { Product = db.Products.FirstOrDefault(p => p.ID.Equals(product)), Quantity = 1 });
            Session["cart"] = products;
            ViewBag.cart = products.Count();
            Session["count"] = Convert.ToInt32(Session["count"]) + 1;

        }
        public void AddOne(int product, List<ItemModel> products, int index)
        {
            products[index].Quantity++;
            ViewBag.cart = products.Count();
            Session["count"] = Convert.ToInt32(Session["count"]) + 1;
        }
        public ActionResult RemoveOne(int product)
        {
            int index = Find(product);
            List<ItemModel> products = (List<ItemModel>)Session["cart"];
            if (products[index].Quantity > 1)
            {
                products[index].Quantity--;
                ViewBag.cart = products.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            }

            return RedirectToAction("ShoppingCart", "Store");
        }
        private int Find(int product)
        {
            List<ItemModel> products = (List<ItemModel>)Session["cart"];
            for (int i = 0; i < products.Count; i++)
                if (products[i].Product.ID.Equals(product))
                    return i;
            return -1;
        }
        public ActionResult Remove(int product)
        {

            int index = Find(product);
            List<ItemModel> products = (List<ItemModel>)Session["cart"];
            Session["count"] = Convert.ToInt32(Session["count"]) - products[index].Quantity;
            products.RemoveAll(p => p.Product.ID.Equals(product));
            Session["cart"] = products;

            return RedirectToAction("ShoppingCart", "Store");
        }
        public ActionResult ShoppingCart()
        {
            return View();

        }

        private Payment payment;

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };
            List<ItemModel> Order = (List<ItemModel>)Session["cart"];
            int totaal = 0;
            foreach (var item in Order)
            {
                int prijs = Convert.ToInt16(Math.Round(item.Product.Price));
                listItems.items.Add(new Item()
                {
                    name = item.Product.Name,
                    currency = "EUR",
                    price = prijs.ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = "sku"
                });
                totaal += prijs * item.Quantity;
                Session["Totaal"] = totaal;
            }
            var payer = new Payer() { payment_method = "paypal" };
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = Order.Sum(item => Convert.ToInt16(Math.Round(item.Product.Price)) * item.Quantity).ToString()
            };
            var amount = new Amount()
            {
                currency = "EUR",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(),
                details = details
            };
            var transactionList = new List<Transaction>();
            transactionList.Add(new Transaction()
            {
                description = "Testing Transaction",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = listItems
            });

            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);

        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId,

            };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apiContext, paymentExecution);
        }

        public ActionResult PaymentWithPayPal()
        {
            if (System.Web.HttpContext.Current.Request.Cookies["UserEMail"] != null)
            {
                APIContext apiContext = PayPalConfiguration.GetAPIContext();
                try
                {
                    //string payerId = Request.Cookies["UserID"].Value;
                    string payerId = Request.Params["PayerID"];
                    if (string.IsNullOrEmpty(payerId))
                    {
                        string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Store/PaymentWithPayPal?";
                        //string baseURI = "http://localhost:5696/Store/ShoppingCart/PaymentWithPayPal?";
                        var guid = Convert.ToString((new Random()).Next(100000));
                        var createdPayment = CreatePayment(apiContext, baseURI + "guid=" + guid);

                        var links = createdPayment.links.GetEnumerator();
                        string paypalRedirectUrl = string.Empty;

                        while (links.MoveNext())
                        {
                            Links link = links.Current;
                            if (link.rel.ToLower().Trim().Equals("approval_url"))
                            {
                                paypalRedirectUrl = link.href;
                            }
                        }
                        Session.Add(guid, createdPayment.id);
                        return Redirect(paypalRedirectUrl);
                    }
                    else
                    {
                        var guid = Request.Params["guid"];
                        var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                        if (executedPayment.state.ToLower() != "approved")
                        {
                            return View("Failure");
                        }
                    }
                }
                catch (Exception)
                {
                    return View("Failure");
                }
                CreateOrder();
                OrderMail();

                Session["cart"] = null;
                Session["count"] = null;
                
                
                return View("Success");
            }
            ViewBag.Message = "U moet ingelogd zijn om de producten te kunnen afrekenen.";
            return View("ShoppingCart");

        }
        public ActionResult Success()
        {

            return View();
        }


        public void CreateOrder()
        {
            int UserID = Convert.ToInt16(System.Web.HttpContext.Current.Request.Cookies["UserID"].Value);
            Guid g = Guid.NewGuid();
            Models.Order order = new Models.Order();
            DateTime dateTime = DateTime.Today;

            order.UserID = UserID;
            order.OrderNumber = g.ToString();
            order.OrderDate = dateTime;
            order.OrderStatus = "Betaald";
            db.Orders.Add(order);
            db.SaveChanges();

            List<ItemModel> products = (List<ItemModel>)Session["cart"];
            //var NewOrder = new Models.Order();
            var NewOrder = db.Orders.FirstOrDefault(o => o.OrderNumber.Equals(g.ToString()));
            int OrderID = NewOrder.ID;

            foreach(ItemModel product in products)
            {
                OrderProducts(product, OrderID);
            }
            db.SaveChanges();
        }
        public void OrderProducts(ItemModel product, int OrderID)
        {   
            OrderProduct OP = new OrderProduct();
            OP.OrderID = OrderID;
            OP.ProductID = product.Product.ID;
            OP.Quantity = product.Quantity;
            db.OrderProducts.Add(OP);
            
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> OrderMail()
        public void OrderMail()
        {
            ModelState.Clear();
            string usermail = System.Web.HttpContext.Current.Request.Cookies["UserEMail"].Value;
            int totaal = (int)Session["Totaal"];
            var body = new StringBuilder();
            body.AppendLine("Uw bestelling is voltooid!  <br />");
            body.AppendLine("U zult een email ontvangen zodra uw bestelling onderweg is. <br />");
            body.AppendLine("Bestelling:  <br />");
            body.AppendLine("Totaal prijs: €" + totaal.ToString());
            
            var message = new MailMessage();
            message.To.Add(new MailAddress(usermail));  // replace with valid value 
            message.From = new MailAddress("DHDomotica@outlook.com");  // replace with valid value
            message.Subject = "Bestelling";
            message.Body = body.ToString();
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {

                smtp.Send(message);
                ViewBag.SuccessMessage = "Uw account is geregistreerd";

            }
           
        }

        public ActionResult Review(int productid)
        {
            ViewBag.product = db.Products.First(p => p.ID.Equals(productid));
            return View();
        }

        public ActionResult SaveReview(int Sterren, string ReviewText, int productid)
        {
            int userid = Convert.ToInt16(System.Web.HttpContext.Current.Request.Cookies["UserID"].Value);
            Review review = db.Reviews.FirstOrDefault(r => r.ProductID.Equals(productid) && r.UserID.Equals(userid));
            if (review == null)
            {
                review = new Review();
                review.ProductID = productid;
                review.UserID = Convert.ToInt16(System.Web.HttpContext.Current.Request.Cookies["UserID"].Value);
                review.Stars = Sterren;
                review.Date = DateTime.Now;
                review.ReviewText = ReviewText;
                db.Reviews.Add(review);
            }
            else
            {
                db.Reviews.FirstOrDefault(r => r.ProductID.Equals(productid) && r.UserID.Equals(userid)).ReviewText = ReviewText;
                db.Reviews.FirstOrDefault(r => r.ProductID.Equals(productid) && r.UserID.Equals(userid)).Stars = Sterren;
            }
            db.SaveChanges();

            return RedirectToAction("ReviewSuccesView", "Store");
        }
        public ActionResult ReviewSuccesView()
        {
            return View();
        }

    }
    }