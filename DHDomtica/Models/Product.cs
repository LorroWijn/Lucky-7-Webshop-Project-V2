using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHDomtica.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int MainCatID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string description { get; set; }
    }
}