using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DHDomtica.Models;

namespace DHDomtica.ViewModels
{
    public class ProductReviewsViewModel
    {
        public Product Product { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}