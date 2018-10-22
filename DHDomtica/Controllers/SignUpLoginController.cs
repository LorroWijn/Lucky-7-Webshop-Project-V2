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
        /*// GET: SignUpLogin
        public ActionResult Index()
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
                //DHDomtica.Models.SignUp en = new Models.SignUp();
                //en.SignUpUser(li);
                //ViewBag.name = li.NickName;

                // return View();  
                //return View("SubmitData");
            }
            else
            {
                return View("Signup");
            }

        }
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
        }*/
    }
}