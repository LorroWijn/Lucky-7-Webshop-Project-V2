using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;

namespace DHDomtica.Controllers
{
    public class StoreController : Controller
    {
        private DHDomoticaDBEntities db = new DHDomoticaDBEntities();

        // GET: Store
        public ViewResult Index()
        {
            return View(db.Product.ToList());
        }
    }
}