using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DHDomtica.Models;
using DHDomtica.Supportclasses;
using System.Collections;
using System.Web.Helpers;

namespace DHDomtica.ViewModels
{
    public class AdminStatisticsViewModel
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }

        internal void ShowMonthlyStatistics()
        {
            int monthnr = 11;
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                var month = DHDomoticadbModel.Statistics.Where(z => z.Month == monthnr);
                // var data = DHDomoticadbModel.Query("SELECT Name, Price FROM Product");
                var myChart = new Chart(width: 600, height: 400)
                    .AddTitle("Bezoekers per maand")
                    .DataBindTable(dataSource: month, xField: "Month")
                    //.AddSeries("Default",
                    //xValue: data, xField: "Frequency",
                    //yValues: data, yFields: "Price")
                    // Aangezien frequency niet bestaat in onze database kan de vorige functie ook niet?
                    .Write();
                    // De vraag is of deze write iets doet
            }
        }

        public AdminStatisticsViewModel()
        {

        }
    }

    
}