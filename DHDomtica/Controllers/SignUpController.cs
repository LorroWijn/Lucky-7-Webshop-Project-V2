using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using DHDomtica.ViewModels;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Linq.Mapping;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Net.Mail;
using System.Threading.Tasks;

// Deze en andere code voor sign-up zijn gebaseerd op de voorbeeldcode van CodAffection bereikbaar op https://www.youtube.com/watch?v=xBS9FMF2NFM

namespace DHDomtica.Controllers
{
    public class SignUpController : Controller
    {
        [HttpGet]
        // GET: SignUp
        public ActionResult SignUpPage(int id = 0)
        {
            var usermodel = new SignUpViewModel();
            return View(usermodel);
        }

        // POST: SignUp/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUpPage(SignUpViewModel signupUserModel)
        {
            if (signupUserModel.EmailInUse() == true)
            {
                ViewBag.DuplicateMessage = "E-mail is al in gebruik. Probeer een ander E-mailadres";
                return View("SignUpPage", signupUserModel);
            }
            else
            {
                signupUserModel.CreateNewUser();


                ModelState.Clear();
                var body = "Uw account voor de website DHDomotica is succesvol aangemaakt.";
                var message = new MailMessage();
                message.To.Add(new MailAddress(signupUserModel.EMail));  // replace with valid value 
                message.From = new MailAddress("DHDomotica@outlook.com");  // replace with valid value
                message.Subject = "Account registratie";
                message.Body = body;
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {

                    await smtp.SendMailAsync(message);
                    ViewBag.SuccessMessage = "Uw account is geregistreerd";
                    //return View("SignUpPage", new User());
                    return RedirectToAction("SignInPage", "SignIn");
                }
            }
        }
    }
}