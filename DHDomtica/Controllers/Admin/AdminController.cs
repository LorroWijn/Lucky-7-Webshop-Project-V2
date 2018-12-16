using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using DHDomtica.ViewModels;
using DHDomtica.Supportclasses;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;

namespace DHDomtica.Controllers
{
    public class AdminController : Controller
    {
        // Alles van statistieken hierna
        // GET: Admin

        //public ActionResult Index()
        //{
        //    ShowAdminSidebar();
        //    return View();
        //}

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
 
        // GET: Home
        public ActionResult Index()
        {
            List<DataPoint> MonthlyVisitors = new List<DataPoint>();
            //List<DataPoint> dataPoints2 = new List<DataPoint>();
            //List<DataPoint> dataPoints3 = new List<DataPoint>();

            MonthlyVisitors.Add(new DataPoint("Jan", 72));
            MonthlyVisitors.Add(new DataPoint("Feb", 67));
            MonthlyVisitors.Add(new DataPoint("Mar", 55));
            MonthlyVisitors.Add(new DataPoint("Apr", 42));
            MonthlyVisitors.Add(new DataPoint("May", 40));
            MonthlyVisitors.Add(new DataPoint("Jun", 35));

            //dataPoints2.Add(new DataPoint("Jan", 48));
            //dataPoints2.Add(new DataPoint("Feb", 56));
            //dataPoints2.Add(new DataPoint("Mar", 50));
            //dataPoints2.Add(new DataPoint("Apr", 47));
            //dataPoints2.Add(new DataPoint("May", 65));
            //dataPoints2.Add(new DataPoint("Jun", 69));

            //dataPoints3.Add(new DataPoint("Jan", 38));
            //dataPoints3.Add(new DataPoint("Feb", 46));
            //dataPoints3.Add(new DataPoint("Mar", 55));
            //dataPoints3.Add(new DataPoint("Apr", 70));
            //dataPoints3.Add(new DataPoint("May", 77));
            //dataPoints3.Add(new DataPoint("Jun", 91));

            ViewBag.MonthlyVisitors = JsonConvert.SerializeObject(MonthlyVisitors);
            //ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            //ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);

            ShowAdminSidebar();
            return View();
        }
    }

    // Waarschijnlijk moet dit gedeelte al in de get van de pagina gedownload worden
    //public ActionResult Index(AdminStatisticsViewModel adminstatistics)
    //{

    //}

    //private DHDomoticaDBEntities Dailydb = new DHDomoticaDBEntities();
    //public ActionResult DailyVisitorsChart()
    //{
    //    ShowAdminSidebar();
    //    return View();
    //}

    //private DHDomoticaDBEntities Monthlydb = new DHDomoticaDBEntities();
    //public ActionResult MonthlyVisitorsChart()
    //{
    //    ShowAdminSidebar();
    //    return View(Monthlydb.Statistics.ToList());
    //}

    //private DHDomoticaDBEntities Yearlydb = new DHDomoticaDBEntities();
    //public ActionResult YearlyVisitorsChart()
    //{
    //    ShowAdminSidebar();
    //    return View();
    //}

    // Nog wel code voor wachtwoord veranderen en uitloggen toevoegen.

}
