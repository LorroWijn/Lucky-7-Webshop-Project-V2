using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using DHDomtica.ViewModels;
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

        // Plaatje kan misschien makkelijk geladen worden in deze pagina door alleen de cshtml te laten zien met het plaatje erin.
        //public ActionResult Index()
        //{
        //    ShowAdminSidebar();
        //    var Monthchart = new AdminStatisticsViewModel();
        //    return View(Monthchart);
        //}

        //Code for the AdminsideBar
        private void ShowAdminSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"AdminSidebar {Request.RawUrl}");
            ViewBag.ShowAdminSideBar = true;
        }

        // Waarschijnlijk moet dit gedeelte al in de get van de pagina gedownload worden
        //public ActionResult Index(AdminStatisticsViewModel adminstatistics)
        //{

        //}

        public ActionResult DailyVisitorsChart()
        {
            return View();
        }

        public ActionResult MonthlyVisitorsChart()
        {
            return View();
        }

        public ActionResult YearlyVisitorsChart()
        {
            return View();
        }
        
        // Nog wel code voor wachtwoord veranderen en uitloggen toevoegen.

    }
}
