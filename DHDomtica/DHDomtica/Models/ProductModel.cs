using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace DHDomtica.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public double Price { get; set; }
        public string ImagePath { get; set; }

    }
}