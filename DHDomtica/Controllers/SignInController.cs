//Sign in 2.0
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using DHDomtica.Models;
using DHDomtica.Supportclasses;

namespace DHDomtica.Controllers
{
    public class SignInController : Controller
    {

        [HttpGet]
        // GET: SignUp
        public ActionResult SignInPage(int id = 0)
        {
            User usermodel = new User();
            return View(usermodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignInPage(User userModel)
        {
            var cookieExpDate = DateTime.UtcNow.AddDays(2);
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                if (userModel.Password == null || userModel.EMail == null)
                {
                    //geen geldige credentials ingevoerd (missend wachtwoord OF missend EmailAddress)
                    return View("SignInPage", new User());
                }
                else
                {
                    var ePwd = Crypto.Hash(userModel.Password);
                    var x = DHDomoticadbModel.Users.FirstOrDefault(u => u.EMail == userModel.EMail && u.Password == ePwd);
                    if (x == null)
                    {
                        //geen geldige credentials ingevoerd (geen combinatie van email + ww)
                        return View("SignInPage", new User());
                    }
                    else
                    {
                        if (x != null) //Moet aan de checkbox worden verbonden. als deze niet gecheckt is komt hij in het bovenste, zowel dan in het onderste
                        {
                            //Geldige credentials ingevoerd
                            //Create Cookie
                            HttpCookie UserCookie = new HttpCookie("UserEMail", x.EMail);
                            HttpCookie PwCookie = new HttpCookie("UserPw", x.Password);
                            HttpCookie NameCookie = new HttpCookie("UserName", x.FirstName);
                            HttpCookie IDCookie = new HttpCookie("UserID", x.ID.ToString());
                            //HttpCookie UserNameCookie = new HttpCookie("UsersName", userModel.FirstName.ToString());                            
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
                            //Geldige credentials ingevoerd (en de checkbox aangevinkt)
                            //Create Cookie
                            HttpCookie UserCookie = new HttpCookie("UserEMail", x.EMail);
                            HttpCookie PwCookie = new HttpCookie("UserPw", x.Password);
                            HttpCookie NameCookie = new HttpCookie("UserName", x.FirstName);
                            HttpCookie IDCookie = new HttpCookie("UserID", x.ID.ToString());

                            //Save data at Cookies
                            HttpContext.Response.SetCookie(UserCookie);
                            HttpContext.Response.SetCookie(PwCookie);
                            HttpContext.Response.SetCookie(NameCookie);
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