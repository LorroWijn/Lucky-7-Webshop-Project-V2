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
            //Zet de jaar, maand en dag op de komende huidige dag
            var year = DateTime.UtcNow.Day;
            var month = DateTime.UtcNow.Month;
            var day = DateTime.UtcNow.Year;
            var number = 0;

            List<DataPoint> MonthlyVisitors = new List<DataPoint>();
            //List<DataPoint> dataPoints2 = new List<DataPoint>();
            //List<DataPoint> dataPoints3 = new List<DataPoint>();

            Random random = new Random();
            

            // De forloop zorgt ervoor dat er 30 dagen worden gecontroleerd in de database en deze in de array zetten om een plaatje ervan te maken
            for (int i = 30; i >= 0; i--)
            {
                // Selecteerd de totaal aantal unieke id's op de dag 
                //var g = from s = statistics

                //where day == s.day
                //&& month == s.month
                //&& year == s.year
                //MonthlyVisitors.add[g] // Y waarde van de grafiek - de eerste waarde komt helemaal links te staan op de horizontale axis en de hoogte is de totale waarde
                //arrayDate.add[year, month, day] // X waarde van de grafiek

                var test = DateTime.UtcNow.AddDays(-i);
                year = test.Year;
                month = test.Month;
                day = test.Day;

                string stringDay = test.Day.ToString();
                string stringMonth = test.ToString("MMM");

                using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
                {
                    var hello = DHDomoticadbModel.Statistics.Where(z => z.Month == month && z.Year == year && z.Day == day).ToArray();
                    number = hello.Length;
                    DHDomoticadbModel.SaveChanges();
                }

                double randomNumber = random.Next(0, 10);

                MonthlyVisitors.Add(new DataPoint(stringDay + " " +  stringMonth, number));
            }

            //MonthlyVisitors.Add(new DataPoint("Jan", 72));
            //MonthlyVisitors.Add(new DataPoint("Feb", 67));
            //MonthlyVisitors.Add(new DataPoint("Mar", 55));
            //MonthlyVisitors.Add(new DataPoint("Apr", 42));
            //MonthlyVisitors.Add(new DataPoint("May", 40));
            //MonthlyVisitors.Add(new DataPoint("Jun", 35));

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


        public string getData()
        {

            return "hello";
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
