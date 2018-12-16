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
        public ActionResult PasswordRecovery(int id = 0)
        {
            //PasswordRecoveryViewModel userModel = new PasswordRecoveryViewModel();
            return View();
        }

        [HttpPost]
        public ActionResult PasswordRecovery(PasswordRecoveryViewModel password)
        {
            var Input = "test@test.nl";
            if (password.EmailCheck(Input) == false)
            {
                ViewBag.NoMailMessage = "E-mail bestaat niet. Probeer een ander E-mailadres";
                return View("PasswordRecovery", Input);
            }
            else
            {
                return View();
            }           
        }
    }
}

