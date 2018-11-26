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
        // GET: UserProfile
        // Orderstatus is index. Alle code van orderstatus hierna
        public ActionResult Index()
        {
            ShowUserSidebar();
            return View();
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
                HttpCookie IDCookie = Request.Cookies["UserID"];
                //HttpCookie UserNameCookie = new HttpCookie("UserName", userModel.FirstName.ToString());                            
                //Expire Date of made cookie
                UserCookie.Expires = cookieExpDate;
                PwCookie.Expires = cookieExpDate;
                NameCookie.Expires = cookieExpDate;
                IDCookie.Expires = cookieExpDate;

                //Save data at Cookies
                HttpContext.Response.SetCookie(UserCookie);
                HttpContext.Response.SetCookie(PwCookie);
                HttpContext.Response.SetCookie(NameCookie);
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
    }
}