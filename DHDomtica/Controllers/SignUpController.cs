﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using DHDomtica.Supportclasses;
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
            User usermodel = new User();
            return View(usermodel);
        }

        // GET: SignUp/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SignUp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SignUp/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUpPage(User userModel)
        {
            userModel.Password = Crypto.Hash(userModel.Password);
            userModel.ConfirmPassword = Crypto.Hash(userModel.ConfirmPassword);
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                if (DHDomoticadbModel.User.Any(x => x.EMail == userModel.EMail))
                {
                    ViewBag.DuplicateMessage = "E-mail is al in gebruik. Probeer een ander E-mailadres";
                    return View("SignUpPage", userModel);
                }

                DHDomoticadbModel.User.Add(userModel);
                DHDomoticadbModel.SaveChanges();
            }
            ModelState.Clear();
            var body = "Uw account voor de website DHDomotica is succesvol aangemaakt.";
            var message = new MailMessage();
            message.To.Add(new MailAddress(userModel.EMail));  // replace with valid value 
            message.From = new MailAddress("DHDomotica@outlook.com");  // replace with valid value
            message.Subject = "Your email subject";
            message.Body = body;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "DHDomotica@outlook.com",  // replace with valid value
                    Password = "DHDadmin"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
                ViewBag.SuccessMessage = "Uw account is geregistreerd";
                //return View("SignUpPage", new User());
                return RedirectToAction("SignInPage", "SignIn");
            }
        }
        // GET: SignUp/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SignUp/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("SignUpPage");
            }
            catch
            {
                return View();
            }
        }

        // GET: SignUp/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SignUp/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("SignUpPage");
            }
            catch
            {
                return View();
            }
        }
    }
}