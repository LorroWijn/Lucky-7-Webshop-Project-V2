
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DHDomtica.Models
{
    public class MainCategoryViewModel
    {
        public MainCategory Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}