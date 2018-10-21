using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Controllers;
using DHDomtica.Models;

namespace DHDomtica.Controllers
{
    public class SignUpLoginController : Controller
    {
        // GET: SignUpLogin
        public ActionResult SignUpLogin()
        {
            return View();
        }

        public ActionResult Signup(DHDomtica.Models.Login li)
        {


            return View(li);
        }

        public ActionResult SubmitData(DHDomtica.Models.Login li)
        {
            if (ModelState.IsValid)
            {
                DHDomtica.Models.SignUp en = new Models.SignUp();
                en.SignUpUser(li);
                ViewBag.name = li.UserNickName;

                // return View();  
                return View("InlogPage");
            }
            else
            {
                return View("SignUpPage");
            }

        }

        // Volgende stukjes moeten aagepast worden aan waarden van loginpage, dus de cshtml file
        public ActionResult login(DHDomtica.Models.Login li)
        {
            return View(li);

        }
        public ActionResult Loginsearch(DHDomtica.Models.Login li)
        {
            DHDomtica.Models.SearchUser ss = new Models.SearchUser();
            string pass = ss.searchuser(li);

            if (pass == li.UserPassword)
            {

                return View("loadlogin");

            }
            @ViewBag.data = "invalide user";
            return View("login");

        }

        public ActionResult loadlogin()
        {

            return View();
        }
    }
}