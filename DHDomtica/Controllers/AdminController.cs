using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using DHDomtica.Supportclasses;
using System.Data.Entity.Infrastructure;

namespace DHDomtica.Controllers
{
    public class AdminController : Controller
    {
        // Alles van statistieken hierna
        // GET: Admin
        public ActionResult Index()
        {
            ShowAdminSidebar();
            return View();
        }

        //Code for the AdminsideBar
        private void ShowAdminSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"AdminSidebar {Request.RawUrl}");
            ViewBag.ShowAdminSideBar = true;
        }

        // Nog wel code voor wachtwoord veranderen en uitloggen toevoegen.

    }
}
