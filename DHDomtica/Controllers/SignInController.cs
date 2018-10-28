//using System.Linq;
//using System.Web.Mvc;
//using DHDomtica.Models;

//namespace MvcLoginAppDemo.Controllers
//{
//    public class SignInController : Controller
//    {
//        public ActionResult InlogPage()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult InlogPage(User userModel)
//        {
//            if (ModelState.IsValid)
//            {
//                using (DHDomoticaDBEntities db = new DHDomoticaDBEntities())
//                {
//                    var obj = db.User.Where(a => a.EMail.Equals(userModel.EMail) && a.Password.Equals(userModel.Password)).FirstOrDefault();
//                    if (obj != null)
//                    {
//                        Session["UserID"] = obj.ID.ToString();
//                        Session["UserEmail"] = obj.EMail.ToString();
//                        return RedirectToAction("UserDashBoard");
//                    }
//                }
//            }
//            return View(userModel);
//        }

//        public ActionResult UserDashBoard()
//        {
//            if (Session["UserID"] != null)
//            {
//                return View();
//            }
//            else
//            {
//                return RedirectToAction("InlogPage");
//            }
//        }
//    }
//}

//Sign in 2.0
using System.Linq;
using System.Web.Mvc;
using DHDomtica.Models;
using DHDomtica.Supportclasses;

namespace DHDomtica.Controllers
{
    public class SignInController : Controller
    {

        [HttpGet]
        // GET: SignUp
        public ActionResult InlogPage(int id = 0)
        {
            User usermodel = new User();
            return View(usermodel);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public ActionResult Inloggen(User userModel)
        {
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                var ePwd = Crypto.Hash(userModel.Password);
                var x = DHDomoticadbModel.User.FirstOrDefault(u => u.EMail == userModel.EMail && u.Password == ePwd);

                if (x == null)
                {
                    //return View("Store/Index", new User());
                    return View("Index", new User());
                    // "Yo werkt niet maat";
                }
                else
                {
                    //je bent ingelogd
                    //return View("Home/InlogPage", new User());
                    return View("Index", new User());
                }
            }
        }
    }
}