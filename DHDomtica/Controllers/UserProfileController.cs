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
            User userModel = new User();
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                userModel = DHDomoticadbModel.Users.Where(z => z.ID == idCheck).FirstOrDefault();
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
                    var p = DHDomoticadbModel.Users.Where(u => u.ID == idCheck).FirstOrDefault();
                    SignUpViewModel usermodel = new SignUpViewModel(p);
                    ShowUserSidebar();
                    return View(usermodel);

                }
            }
            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePersonalInformation(SignUpViewModel userModel)
        {
            userModel.ChangeExistingUser();
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
            //        }
            //    }
            //}
        }
    }
}