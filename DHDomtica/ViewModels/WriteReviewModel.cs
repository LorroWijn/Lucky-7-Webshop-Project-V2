using DHDomtica.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DHDomtica.ViewModels
{
    public class WriteReviewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Stars")]
        public int Stars { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Review")]
        public string ReviewText { get; set; }

        [DataType(DataType.Date)]
        public int Date { get; set; }

        internal void CreateReview()
        {
            Review review = new Review()
            {
                ID = ID,
                UserID = UserID,
                ProductID = ProductID,
                Stars = Stars,
                ReviewText = ReviewText,
                Date = DateTime.Now
            };

            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                DHDomoticadbModel.Reviews.Add(review);
                DHDomoticadbModel.SaveChanges();
            }


        }

        public WriteReviewModel()
        {
        }

    }
}