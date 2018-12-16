using System;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using DHDomtica.Models;
using DHDomtica.ViewModels;
using DHDomtica.Supportclasses;

namespace DHDomtica.Controllers
{
    public class PasswordRecoveryController : Controller
    {
        // GET: PasswordRecovery
        public ActionResult PasswordRecovery()
        {
            //PasswordRecoveryViewModel userModel = new PasswordRecoveryViewModel();
            return View();
        }

        [HttpPost]
        public ActionResult PasswordRecovery(PasswordRecoveryViewModel password)
        {
            var Input = password.EMail;
            var Check = password.EmailCheck(Input);
            if (Check)
            {
                ViewBag.NoMailMessage = "E-mail bestaat niet. Probeer een ander E-mailadres";
                return View();
            }
            else
            {
                // Email versturen
                return View();
            }           
        }
    }
}

