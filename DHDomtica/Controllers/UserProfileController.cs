using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using DHDomtica.ViewModels;
using DHDomtica.Supportclasses;
using System.Data.Entity.Infrastructure;

namespace DHDomtica.Controllers
{
    

    public class UserProfileController : Controller
    {
        private DHDomoticaDBEntities db = new DHDomoticaDBEntities();
        // GET: UserProfile
        // Orderstatus is index. Alle code van orderstatus hierna
        public ActionResult Index()
        {
            int userid = Convert.ToInt32(System.Web.HttpContext.Current.Request.Cookies["UserID"].Value);
            List<Order> OrderList = db.Orders.Where(o => 
                o.UserID.Equals(userid) &&
                !o.OrderStatus.Equals("Bezorgd"))
                .ToList();


            ShowUserSidebar();
            return View(OrderList);
        }

        //Code for the UsersideBar
        private void ShowUserSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"UserSidebar {Request.RawUrl}");
            ViewBag.ShowUserSideBar = true;
        }

        // Alles van persoonlijke informatie hierna
        [HttpGet]
        public ActionResult PersonalInformation()
        {
            var con = System.Web.HttpContext.Current.Request.Cookies;
            var idCheck = Convert.ToInt32(con["UserID"].Value);
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                User user = DHDomoticadbModel.Users.Where(z => z.ID == idCheck).FirstOrDefault();
                SignUpViewModel userModel = new SignUpViewModel(user);
                ShowUserSidebar();
                return View(userModel);
                // Moet nog aangepast worden zodat informatie uit de cookies gehaald wordt.
            }
        }

        // Alles van change personal information hierna
        public ActionResult ChangePersonalInformation()
        // Moet waarschijnlijk verwijzing naar user bij
        {
            var con = System.Web.HttpContext.Current.Request.Cookies;
            var idCheck = Convert.ToInt32(con["UserID"].Value);
            var pwdCheck = con["UserPw"].Value;
            if (con["UserID"] != null)
            {
                using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
                {
                    User ss = DHDomoticadbModel.Users.Where(u => u.ID == idCheck).FirstOrDefault();
                    SignUpViewModel userModel = new SignUpViewModel(ss);
                    ShowUserSidebar();
                    return View(userModel);

                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePersonalInformation(SignUpViewModel userModel)
        {
            userModel.ChangeExistingUser();

            HttpCookie NameCookie = new HttpCookie("UserName", userModel.FirstName);
            HttpContext.Response.SetCookie(NameCookie);
            NameCookie.Expires = DateTime.UtcNow.AddDays(2);

            HttpCookie EmailCookie = new HttpCookie("UserEMail", userModel.FirstName);
            HttpContext.Response.SetCookie(EmailCookie);
            NameCookie.Expires = DateTime.UtcNow.AddDays(2);

            HttpCookie LastNameCookie = new HttpCookie("UserLast", userModel.LastName);
            HttpContext.Response.SetCookie(LastNameCookie);
            LastNameCookie.Expires = DateTime.UtcNow.AddDays(2);
            return RedirectToAction("Index", "Home");
        }
        //return RedirectToAction("PersonalInformation", "UserProfile");


        [HttpGet]
        //Alles van wachtwoord veranderen hierna
        public ActionResult ChangePassword()
        {
            ShowUserSidebar();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserPasswordChange userModel)
        {
            int idCook = Convert.ToInt32(Request.Cookies["UserID"].Value);
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                var x = DHDomoticadbModel.Users.FirstOrDefault(u => u.ID == idCook);
                var oldPw = Crypto.Hash(userModel.OldPassword);
                var chPw = userModel.ChangePassword;
                var chPwVer = userModel.ChangeConfirmPassword;
                if (x.Password == oldPw && chPw == chPwVer)
                {
                    chPw = Crypto.Hash(chPw);
                    x.Password = chPw;

                    HttpCookie PwCookie = new HttpCookie("UserPw", x.Password);
                    PwCookie.Expires = DateTime.UtcNow.AddDays(2);
                    HttpContext.Response.SetCookie(PwCookie);

                    DHDomoticadbModel.SaveChanges();
                    return RedirectToAction("PersonalInformation", "UserProfile");
                }
                else
                {
                    ViewBag.WrongPasswordMessage = "Uw oude wachtwoord is niet goed ingevuld";
                    ShowUserSidebar();
                    return View("ChangePassword", userModel);
                }
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            ShowUserSidebar();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut(string uitloggen)
        {
            var cookieExpDate = DateTime.UtcNow.AddDays(-500);
            if (uitloggen == "Ja, ik wil uitloggen")
            {
                //Create Cookie
                HttpCookie UserCookie = Request.Cookies["UserEMail"];
                HttpCookie PwCookie = Request.Cookies["UserPw"];
                HttpCookie NameCookie = Request.Cookies["UserName"];
                HttpCookie LastNameCookie = Request.Cookies["UserLast"];
                HttpCookie IDCookie = Request.Cookies["UserID"];
                //HttpCookie UserNameCookie = new HttpCookie("UserName", userModel.FirstName.ToString());                            
                //Expire Date of made cookie
                UserCookie.Expires = cookieExpDate;
                PwCookie.Expires = cookieExpDate;
                NameCookie.Expires = cookieExpDate;
                LastNameCookie.Expires = cookieExpDate;
                IDCookie.Expires = cookieExpDate;

                //Save data at Cookies
                HttpContext.Response.SetCookie(UserCookie);
                HttpContext.Response.SetCookie(PwCookie);
                HttpContext.Response.SetCookie(NameCookie);
                HttpContext.Response.SetCookie(LastNameCookie);
                HttpContext.Response.SetCookie(IDCookie);

                //Returns to index page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ShowUserSidebar();
                return RedirectToAction("Index", "UserProfile");

            }
        }
        //Generating ProductList by category ID
        public ActionResult OrderHistory()
        {
            var con = System.Web.HttpContext.Current.Request.Cookies;
            var idCheck = Convert.ToInt32(con["UserID"].Value);
            if (con["UserID"] != null)
            {

                List<Order> OrderList = db.Orders.Where(o =>
                o.UserID.Equals(idCheck) &&
                o.OrderStatus.Equals("Bezorgd")
                ).ToList();
                ShowUserSidebar();
                return View(OrderList);

            }
            ShowUserSidebar();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Order(int OrderID)
        {
            Order order = db.Orders.FirstOrDefault(o => o.ID.Equals(OrderID));
            List<OrderProduct> OP = db.OrderProducts.Where(op => op.OrderID.Equals(order.ID)).ToList();
            List<ItemModel> OrderProducts = new List<ItemModel>();
            Session["Order"] = order;
            foreach (OrderProduct item in OP)
            {
                ItemModel product = new ItemModel()
                {
                    Product = db.Products.FirstOrDefault(p => p.ID.Equals(item.ProductID)),
                    Quantity = item.Quantity
                
                };
                OrderProducts.Add(product);
            }

            //ViewBag.Message = "test";
            ShowUserSidebar();
            return View(OrderProducts);
        }
    }
}