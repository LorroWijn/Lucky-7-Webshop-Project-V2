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
using System.Text;

// Deze en andere code voor sign-up zijn gebaseerd op de voorbeeldcode van CodAffection bereikbaar op https://www.youtube.com/watch?v=xBS9FMF2NFM

namespace DHDomtica.Controllers
{
    public class SignUpController : Controller
    {
        private DHDomoticaDBEntities db = new DHDomoticaDBEntities();
        [HttpGet]
        // GET: SignUp
        public ActionResult SignUpPage(int id = 0)
        {
            var userModel = new SignUpViewModel();
            return View(userModel);
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
            //else if (signupUserModel.Password != signupUserModel.ConfirmPassword)
            //{
            //    ViewBag.PasswordDifferenceMessage = "Het wachtwoord en bevestiging van het wachtwoord komen niet overeen";
            //    return View("SignUpPage", signupUserModel);
            //}
            else
            {
                signupUserModel.CreateNewUser();

                User user = db.Users.First(u => u.EMail.Equals(signupUserModel.EMail));
                ModelState.Clear();

                var message = new MailMessage();
                message.To.Add(new MailAddress(signupUserModel.EMail));
                message.From = new MailAddress("DHDomotica@outlook.com");
                message.Subject = "Account registratie";
                message.Body = string.Format("Beste " + user.FirstName + ", <BR/> Bedankt voor uw registratie, <BR/> Klik op onderstaande link om uw emailadres te bevestigen: <BR/> <a href =\"{1}\" title =\"User Email Confirm\">{1}</a>",
                signupUserModel.EMail, Url.Action("ConfirmEmail", "SignUp",
                new { Token = user.ID, Email = signupUserModel.EMail }, Request.Url.Scheme)) ;

                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {

                    await smtp.SendMailAsync(message);
                    ViewBag.SuccessMessage = "Uw account is geregistreerd";
                    return RedirectToAction("SignInPage", "SignIn");
                }
            }
        }
        
        public ActionResult ConfirmEmail(int Token, string Email)
        {
            User user = db.Users.First(u => u.ID.Equals(Token));

                if (user.EMail == Email)
                {
                    user.EmailConfirmed = true;
                    db.Users.First(u => u.ID.Equals(Token)).EmailConfirmed = true;
                    db.SaveChanges();
                    
                    return RedirectToAction("SignInPage", "Home" );
                }
                else
                {
                    return RedirectToAction("Failure", "Account");
                }
            }
           
        
    }
}