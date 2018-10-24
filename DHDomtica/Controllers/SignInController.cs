using System.Linq;
using System.Web.Mvc;
using DHDomtica.Models;

namespace MvcLoginAppDemo.Controllers
{
    public class SignInController : Controller
    {
        public ActionResult InlogPage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InlogPage(User userModel)
        {
            if (ModelState.IsValid)
            {
                using (DHDomoticaDBEntities db = new DHDomoticaDBEntities())
                {
                    var obj = db.User.Where(a => a.EMail.Equals(userModel.EMail) && a.Password.Equals(userModel.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.ID.ToString();
                        Session["UserEmail"] = obj.EMail.ToString();
                        return RedirectToAction("UserDashBoard");
                    }
                }
            }
            return View(userModel);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("InlogPage");
            }
        }
    }
}