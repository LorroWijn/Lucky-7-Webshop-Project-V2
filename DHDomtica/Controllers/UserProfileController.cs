using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
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
        public ActionResult PersonalInformation(int? id)
        {
            User userModel = new User();
            using(DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                userModel = DHDomoticadbModel.User.Where(z => z.ID == id).FirstOrDefault();
                ShowUserSidebar();
                return View(userModel);
            }
        }

        // Alles van change personal information hierna
        public ActionResult ChangePersonalInformation(int id = 0)
            // Moet waarschijnlijk verwijzing naar user bij
        {
            User usermodel = new User();
            ShowUserSidebar();
            return View();
            // User usermodel regel veranderen in dat deze ingevuld wordt door de database
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePersonalInformation(User userModel)
        {
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                if (userModel.Password == null)
                {
                    //geen wachtwoord ingevuld
                    ViewBag.BadPasswordMessage = "Uw ingevulde wachtwoord komt niet overeen met Uw huidige wachtwoord";
                    return View("ChangePersonalInformation", userModel);
                }
                else
                {
                    var ePwd = Crypto.Hash(userModel.Password);
                    var p = DHDomoticadbModel.User.Where(u => u.Password == ePwd).FirstOrDefault();

                    if (p == null)
                    {
                        //geen goede huidige wachtwoord ingevuld
                        ViewBag.BadPasswordMessage = "Uw ingevulde wachtwoord komt niet overeen met Uw huidige wachtwoord";
                        return View("ChangePersonalInformation", userModel);
                    }
                    else
                    {
                        DHDomoticadbModel.Entry(userModel).State = System.Data.Entity.EntityState.Modified;
                        DHDomoticadbModel.SaveChanges();
                        ModelState.Clear();
                        return RedirectToAction("PersonalInformation", "UserProfile");
                    }
                    
                }
            }
        }

        // Alles van wachtwoord veranderen hierna
        //public ActionResult ChangePassword()
        //{
        //    ShowUserSidebar();
        //    return View();
        //

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SignInPage(User userModel)
        //{
        //    using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
        //    {
        //        if (userModel.Password == null)
        //        {
        //            geen geldige credentials ingevoerd
        //            return View("SignInPage", new User());
        //        }
        //        else
        //        {
        //            var ePwd = Crypto.Hash(userModel.Password);
        //            var x = DHDomoticadbModel.User.FirstOrDefault(u => u.EMail == userModel.EMail && u.Password == ePwd);

        //            if (x == null)
        //            {
        //                geen geldige credentials ingevoerd
        //                return View("SignInPage", new User());
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //    }
        //}
    }
}