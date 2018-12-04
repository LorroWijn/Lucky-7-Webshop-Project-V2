using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
using DHDomtica.ViewModels;
using DHDomtica.Supportclasses;

namespace DHDomtica.Controllers
{
    public class AdminManageUserController : Controller
    {
        private DHDomoticaDBEntities db = new DHDomoticaDBEntities();

        // GET: AdminManageUser
        public ActionResult Index()
        {
            ShowAdminSidebar();
            var adminUserModelEnum = new AdminManageUserViewModel().VMList();
            return View(adminUserModelEnum);
        }

        //Code for the AdminsideBar
        private void ShowAdminSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"AdminSidebar {Request.RawUrl}");
            ViewBag.ShowAdminSideBar = true;
        }

        // GET: AdminManageUser/Create
        public ActionResult Create()
        {
            {
                ShowAdminSidebar();
                ViewBag.AdminID = new SelectList(db.AdminRights, "AdminID", "Rights");
                return View();
            }
        }


        // POST: AdminManageUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminManageUserViewModel adminUserModel)
        {
            //user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
            if (ModelState.IsValid)
            {
                adminUserModel.CreateNewUser();
                ShowAdminSidebar();
                var adminUserModelEnum = new AdminManageUserViewModel().VMList();
                return View("Index", adminUserModelEnum);
            }
            else
            {
                ShowAdminSidebar();
                ViewBag.AdminID = new SelectList(db.AdminRights, "AdminID", "Rights", adminUserModel.AdminID);
                return View("Create", adminUserModel);
            }
        }

        // GET: AdminManageUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            AdminManageUserViewModel adminModel = new AdminManageUserViewModel(user);

            ShowAdminSidebar();
            return View(adminModel);
        }

        // GET: AdminManageUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            AdminManageUserViewModel adminModel = new AdminManageUserViewModel(user);

            ShowAdminSidebar();
            return View(adminModel);
        }

        // POST: AdminManageUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminManageUserViewModel AdminUser)
        {

            using (DHDomoticaDBEntities DHDomoticadbModel = new DHDomoticaDBEntities())
            {
                AdminUser.EditExistingUser(AdminUser.ID);
                ShowAdminSidebar();
                return RedirectToAction("Index");
            }
        }

        // GET: AdminManageUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            AdminManageUserViewModel adminModel = new AdminManageUserViewModel(user);

            ShowAdminSidebar();
            return View(adminModel);
        }

        // POST: AdminManageUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            var wl = db.Wishlists.Where(l => l.UserID.Equals(id));
            var orders = db.Orders.Where(o => o.UserID.Equals(id));

            List<int> orderids = new List<int>();
            foreach (Order o in orders)
            {
                orderids.Add(o.ID);
            }

            var op = db.OrderProducts.Where(p => orderids.Contains(p.OrderID));


            db.Wishlists.RemoveRange(wl);
            db.OrderProducts.RemoveRange(op);
            db.Orders.RemoveRange(orders);
            db.SaveChanges();
            ShowAdminSidebar();
            return RedirectToAction("Index");
        }
    }
}
