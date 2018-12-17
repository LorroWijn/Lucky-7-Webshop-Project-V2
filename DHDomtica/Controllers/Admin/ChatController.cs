using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DHDomtica.Controllers
{
    public class ChatController : Controller
    {
        private DHDomoticaDBEntities db = new DHDomoticaDBEntities();
        // GET: Chat
        public ActionResult Index()
        {

            if (System.Web.HttpContext.Current.Request.Cookies["UserID"] == null)
            {
                return Redirect("/");
            }

            int userid = Convert.ToInt16(System.Web.HttpContext.Current.Request.Cookies["UserID"].Value);
            User currentUser = db.Users.First(u => u.ID == userid);

            List<User> admins = db.Users.Where(u => u.AdminID != 1).ToList();

            

            return View(admins);
        }
        public ActionResult ChatScreen(int contactid)
        {


 
            return View();
            }
        
    }
}