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
        public ActionResult SignInPage(int id = 0)
        {
            User usermodel = new User();
            return View(usermodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignInPage(User userModel)
        {
            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                if (userModel.Password == null)
                {
                    //geen geldige credentials ingevoerd
                    return View("SignInPage", new User());
                }
                else
                {
                    var ePwd = Crypto.Hash(userModel.Password);
                    var x = DHDomoticadbModel.User.FirstOrDefault(u => u.EMail == userModel.EMail && u.Password == ePwd);

                    if (x == null)
                    {
                        //geen geldige credentials ingevoerd
                        return View("SignInPage", new User());
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
        }
    }
}