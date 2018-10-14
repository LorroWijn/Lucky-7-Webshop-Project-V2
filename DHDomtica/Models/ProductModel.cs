using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;

namespace DHDomtica.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImagePath { get; set; }

        public string URL { get; set; }

        //Foreign Key from MainCategroyModel.cs
        public int MainCatId { get; set; }

        //Foreign Key from SubCategroyModel.cs
        public int SubCatId { get; set; }

    }
}