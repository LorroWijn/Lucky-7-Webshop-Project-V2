﻿//Sign in 2.0
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using DHDomtica.Models;
using DHDomtica.ViewModels;
using DHDomtica.Supportclasses;

namespace DHDomtica.Controllers
{
    public class SignInController : Controller
    {

        [HttpGet]
        // GET: SignUp
        public ActionResult SignInPage(int id = 0)
        {
            SignUpViewModel userModel = new SignUpViewModel();
            return View(userModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignInPage(SignUpViewModel userModel)
        {
            var cookieExpDate = DateTime.UtcNow.AddDays(2);
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                if (userModel.Password == null || userModel.EMail == null)
                {
                    //geen geldige credentials ingevoerd (missend wachtwoord OF missend EmailAddress)
                    return View("SignInPage", new SignUpViewModel());
                }
                else
                {
                    var ePwd = Crypto.Hash(userModel.Password);
                    var x = DHDomoticadbModel.Users.FirstOrDefault(u => u.EMail == userModel.EMail && u.Password == ePwd);
                    if (x == null)
                    {
                        //geen geldige credentials ingevoerd (geen combinatie van email + ww)
                        return View("SignInPage", new SignUpViewModel());
                    }
                    else
                    {

                        if (x != null) //Moet aan de checkbox worden verbonden. als deze niet gecheckt is komt hij in het bovenste, zowel dan in het onderste
                        {
                            if (!x.EmailConfirmed.Value)
                            {
                                ViewBag.Message = "U moet eerst uw emailadres bevestigen.";
                                return View("SignInPage", new SignUpViewModel());
                            }
                            //Geldige credentials ingevoerd
                            //Create Cookie
                            HttpCookie UserCookie = new HttpCookie("UserEMail", x.EMail);
                            HttpCookie PwCookie = new HttpCookie("UserPw", x.Password);
                            HttpCookie NameCookie = new HttpCookie("UserName", x.FirstName);
                            HttpCookie LastNameCookie = new HttpCookie("UserLast", x.LastName);
                            HttpCookie IDCookie = new HttpCookie("UserID", x.ID.ToString());
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
                            //Geldige credentials ingevoerd (en de checkbox aangevinkt)
                            //Create Cookie
                            HttpCookie UserCookie = new HttpCookie("UserEMail", x.EMail);
                            HttpCookie PwCookie = new HttpCookie("UserPw", x.Password);
                            HttpCookie NameCookie = new HttpCookie("UserName", x.FirstName);
                            HttpCookie LastNameCookie = new HttpCookie("UserLast", x.LastName);
                            HttpCookie IDCookie = new HttpCookie("UserID", x.ID.ToString());

                            //Save data at Cookies
                            HttpContext.Response.SetCookie(UserCookie);
                            HttpContext.Response.SetCookie(PwCookie);
                            HttpContext.Response.SetCookie(NameCookie);
                            HttpContext.Response.SetCookie(LastNameCookie);
                            HttpContext.Response.SetCookie(IDCookie);
                            //Returns to index page
                            return RedirectToAction("Index", "Home");

                        }
                    }
                }
            }
        }
    }
}