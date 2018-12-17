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
using DHDomtica.Supportclasses;

namespace DHDomtica.Controllers
{

    public class PasswordRecoveryController : Controller
    {
        private DHDomoticaDBEntities db = new DHDomoticaDBEntities();
        // GET: PasswordRecovery
        public ActionResult PasswordRecovery()
        {
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
                RecoveryEmail(Input);
                return RedirectToAction("CheckYourEMail", "PasswordRecovery");
            }           
        }

        // GET: RecoveringPassword
        public ActionResult CheckYourEMail()
        {
            return View();
        }

        // GET: RecoveringPassword
        public ActionResult RecoveringPassword(string Token, string email)
        {
            bool recovery = false;
            User user = new User();
            user = db.Users.FirstOrDefault(u => u.EMail.Equals(email));
            if(user.Recovery == Token)
            {
                recovery = true;
            }
            ViewBag.Recovery = recovery;
            Session["useremail"] = email;
            return View();
        }


        public void RecoveryEmail(string email)
        {
            Guid g = Guid.NewGuid();
            User user = new User();
            user = db.Users.FirstOrDefault(u => u.EMail.Equals(email));
            db.Users.FirstOrDefault(u => u.EMail.Equals(email)).Recovery = g.ToString();
            db.SaveChanges();
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            message.From = new MailAddress("DHDomotica@outlook.com");
            message.Subject = "Wachtwoord reset";
            message.Body = string.Format("Beste " + user.FirstName + ", <BR/> Klik op onderstaande link om uw wachtwoord te veranderen. <br /> <a href =\"{1}\" title =\"Wachtwoord reset\">{1}</a>",
            email, Url.Action("RecoveringPassword", "PasswordRecovery",
            new
            {
                Token = g.ToString(),
                Email = email
            }, Request.Url.Scheme));

            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {

                smtp.Send(message);
            }
        }

        [HttpPost]
        public ActionResult RecoveringPassword(string password, string confirmpassword, string email)
        {
            if (password != confirmpassword)
            {
                ViewBag.WrongPasswordMessage = "Het wachtwoord en de wachtwoordbevestiging komen niet overeen.";
                return View();
            }
            else
            {
                string usermail = (string)Session["useremail"];
                string newPassword = Crypto.Hash(password);
                db.Users.FirstOrDefault(u => u.EMail.Equals(usermail)).Password = newPassword;
                db.Users.FirstOrDefault(u => u.EMail.Equals(usermail)).Recovery = null;
                db.SaveChanges();
                Session["useremail"] = null;

                return RedirectToAction("SignInPage", "SignIn");
            }
        }
    }

}

