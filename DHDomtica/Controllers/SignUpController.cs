using System;
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
        public ActionResult Index(int id = 0)
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
        public ActionResult Index(User userModel)
        {
            using (var DHDomoticadbModel = new DHDomoticaDataContext())
            // If loop met gebruikersnaam gelijk aan gebruikersnaamcheck hangen.
            {
                if (DHDomoticadbModel.Users.Any(x => x.NickName == userModel.NickName))
                {
                    ViewBag.DuplicateMessage = "Gebruikersnaam is al in gebruik. Probeer een andere gebruikersnaam.";
                    return View("Index", userModel);
                }

              //  DHDomoticadbModel.Users.Add(userModel);
               // DHDomoticadbModel.SubmitChanges;
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Uw account is geregistreerd";
            return View("Index", new User());
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

                return RedirectToAction("Index");
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

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}