using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHDomtica.Models;

namespace DHDomtica.ViewModels
{ 
    public class StatisticsViewModel
    {
        public int ID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public string IdSession { get; set; }


        internal void CreateNewStatistic()
        {
            Statistic statistics = new Statistic
            {
                Year = DateTime.UtcNow.Year,
                Month = DateTime.UtcNow.Month,
                Day = DateTime.UtcNow.Day,
                Hour = DateTime.UtcNow.Hour,
                IdSession = HttpContext.Current.Session.SessionID
            };

            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                if(DHDomoticadbModel.Statistics.FirstOrDefault(x => x.IdSession == IdSession) == null)
                {
                    DHDomoticadbModel.Statistics.Add(statistics);
                    DHDomoticadbModel.SaveChanges();
                }
            }
        }

        public StatisticsViewModel()
        {

        }
    }
}