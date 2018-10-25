﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;

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
        public ActionResult SignUpPage(User userModel)
        {
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            // If loop met passwordcheck gelijk aan gebruikersnaamcheck hangen.
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
            ViewBag.SuccessMessage = "Uw account is geregistreerd";
            return View("InlogPage", new User());

            // Migrations moeten waarschijnlijk moeten worden toegevoegd. Migrations kijken of ik ze een datumstempel kan geven.
            // Kijken naar savechanges en Add en hoe dat werkt.
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