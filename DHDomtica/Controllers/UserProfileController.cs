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
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                userModel = DHDomoticadbModel.User.Where(z => z.ID == id).FirstOrDefault();
                ShowUserSidebar();
                return View(userModel);
                // Moet nog aangepast worden zodat informatie uit de cookies gehaald wordt.
            }
        }

        // Alles van change personal information hierna
        public ActionResult ChangePersonalInformation(int id = 0)
        // Moet waarschijnlijk verwijzing naar user bij
        {
            User usermodel = new User();
            ShowUserSidebar();
            return View();
            // User usermodel regel veranderen in dat deze ingevuld wordt door de database.
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePersonalInformation(User userModel)
        {

            return RedirectToAction("PersonalInformation", "UserProfile");
            //using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            //{
            //    if (userModel.Password == null)
            //    {
            //        //geen wachtwoord ingevuld
            //        ViewBag.BadPasswordMessage = "Uw ingevulde wachtwoord komt niet overeen met Uw huidige wachtwoord";
            //        return View("ChangePersonalInformation", userModel);
            //    }
            //    else
            //    {
            //        var ePwd = Crypto.Hash(userModel.Password);
            //        var p = DHDomoticadbModel.User.Where(u => u.Password == ePwd).FirstOrDefault();

            //        if (p == null)
            //        {
            //            //geen goede huidige wachtwoord ingevuld
            //            ViewBag.BadPasswordMessage = "Uw ingevulde wachtwoord komt niet overeen met Uw huidige wachtwoord";
            //            return View("ChangePersonalInformation", userModel);
            //        }
            //        else
            //        {
            //            DHDomoticadbModel.Entry(userModel).State = System.Data.Entity.EntityState.Modified;
            //            DHDomoticadbModel.SaveChanges();
            //            // Misschien ook hier de entries aanpassen, zodat de goede worden gepakt.
            //            ModelState.Clear();
            //            return RedirectToAction("PersonalInformation", "UserProfile");
            //        }

            //    }
            //}



        }

        //Alles van wachtwoord veranderen hierna
        public ActionResult ChangePassword()
        {
            ShowUserSidebar();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignInPage(UserPasswordChange userModel)
        {
            int idCook = Convert.ToInt32(Request.Cookies["UserID"].Value);
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                var x = DHDomoticadbModel.User.FirstOrDefault(u => u.ID == idCook);
                var oldPw = Crypto.Hash(userModel.OldPassword);
                var chPw = userModel.ChangePassword;
                var chPwVer = userModel.ChangeConfirmPassword;
                if (x.Password == oldPw && chPw == chPwVer)
                {
                    x.Password = chPw;
                }
            }
            return RedirectToAction("PersonalInformation", "UserProfile");

            //using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            //{
            //    if (userModel.ChangePassword == null)
            //    {
            //        //Geen wachtwoord ingevuld
            //        ViewBag.BadPasswordMessage = "Uw ingevulde wachtwoord komt niet overeen met Uw huidige wachtwoord";
            //        return View("ChangePassword", userModel);
            //    }
            //    else
            //    {
            //        var cPwd = Crypto.Hash(userModel.OldPassword);
            //        userModel.ChangePassword = Crypto.Hash(userModel.ChangePassword);
            //        userModel.ChangeConfirmPassword = Crypto.Hash(userModel.ChangeConfirmPassword);
            //        var p = DHDomoticadbModel.User.Where(u => u.Password == cPwd).FirstOrDefault();
            //        // var p = DHDomoticadbModel.User.SingleOrDefault(u => u.Password == cPwd);
            //        // Als de vorige niet werkt dan deze proberen.

            //        if (p == null)
            //        {
            //            //Niet goede huidige wachtwoord ingevuld
            //            ViewBag.BadPasswordMessage = "Uw ingevulde wachtwoord komt niet overeen met Uw huidige wachtwoord";
            //            return View("ChangePassword", userModel);
            //        }
            //        else
            //        {
            //            p.Password = Crypto.Hash(userModel.ChangePassword);
            //            p.ConfirmPassword = Crypto.Hash(userModel.ChangePassword);
            //            DHDomoticadbModel.Entry(DHDomoticadbModel.User).State = System.Data.Entity.EntityState.Modified;
            //            // Vorige regel moet aangepast worden aan dat het in de goede model gegooid wordt. DHDomoticadbModel.User of p. Ik weet niet welke goed is.
            //            DHDomoticadbModel.SaveChanges();
            //            ModelState.Clear();
            //            return RedirectToAction("PersonalInformation", "UserProfile");
            //            // The error stems from line entity.Entry(account). 
            //            //Either: 1) UpdateAccount is not a type in your DbContext models OR 
            //            //2) It is a type but you still have to retrieve the instance first OR attach this instance to the DbContext. 
        }
    }
}