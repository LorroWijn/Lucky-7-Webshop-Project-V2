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
        //Code for the AdminsideBar
        private void ShowAdminSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"AdminSidebar {Request.RawUrl}");
            ViewBag.ShowAdminSideBar = true;
        }

        // GET: Home
        [HttpGet]
        public ActionResult Index(string caseSwitch)
        {
            //Zet de jaar, maand en dag op de komende huidige dag
            var year = DateTime.UtcNow.Day;
            var month = DateTime.UtcNow.Month;
            var day = DateTime.UtcNow.Year;
            var number = 0;

            List<DataPoint> MonthlyVisitors = new List<DataPoint>();
            int tabel = 0;
            int increments = 0;
            //string caseSwitch = "1 maand";
            switch (caseSwitch)
            {
                case "Afgelopen Week":
                    tabel = 7;
                    increments = 1;
                    break;
                case "Afgelopen Maand":
                    tabel = 30;
                    increments = 1;
                    break;
                case "Afgelopen 3 Maanden":
                    tabel = 90;
                    increments = 1;
                    break;
                case "Afgelopen Jaar":
                    tabel = 365;
                    increments = 1;
                    break;
            }

            // De forloop zorgt ervoor dat er 30 dagen worden gecontroleerd in de database en deze in de array zetten om een plaatje ervan te maken
            for (int i = tabel; i >= 0; i = i - increments)
            {
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

                MonthlyVisitors.Add(new DataPoint(stringDay + " " +  stringMonth, number));
            }

            ViewBag.MonthlyVisitors = JsonConvert.SerializeObject(MonthlyVisitors);

            ShowAdminSidebar();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Stats stats)
        {
            Console.WriteLine(stats.whichStat);
            ShowAdminSidebar();
            string x = stats.whichStat;
            return View(Index(x));
        }


        public string getData()
        {
            return "hello";
        }
    }
}
