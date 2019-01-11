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
using System.Data.Entity.Infrastructure;

namespace DHDomtica.Controllers
{
    public class AdminManageProductsController : Controller
    {
        private DHDomoticaDBEntities db = new DHDomoticaDBEntities();

        // GET: Products
        public ActionResult Index(string searchString)
        {
            var product = db.Products.Include(p => p.MainCategory);
            if (!string.IsNullOrEmpty(searchString))
            {
                product = db.Products.Where(p => p.Name.Contains(searchString));
            }
            ShowAdminSidebar();
            var adminProductModelEnum = new AdminManageProductViewModel().VMList();
            return View(adminProductModelEnum);

        }
        //Code for the AdminsideBar
        private void ShowAdminSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"AdminSidebar {Request.RawUrl}");
            ViewBag.ShowAdminSideBar = true;
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ShowAdminSidebar();
            var adminProductModelEnum = new AdminManageProductViewModel(product);
            return View(adminProductModelEnum);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.MainCategoryID = new SelectList(db.MainCategories, "ID", "Name");
            ShowAdminSidebar();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Price,Stock,Image,MainCategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                ShowAdminSidebar();
                return RedirectToAction("Index");
            }
            else
            {
                ShowAdminSidebar();
                ViewBag.MainCategoryID = new SelectList(db.MainCategories, "ID", "Name", product.MainCategoryID);
                return View("Create", product);
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.MainCategoryID = new SelectList(db.MainCategories, "ID", "Name", product.MainCategoryID);

            var adminProductModelEnum = new AdminManageProductViewModel(product);
            ShowAdminSidebar();
            return View(adminProductModelEnum);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Price,Stock,Image,MainCategoryID")] Product product)
        {
            if (!ModelState.IsValid)
            {
                ShowAdminSidebar();
                ViewBag.MainCategoryID = new SelectList(db.MainCategories, "ID", "Name", product.MainCategoryID);
                return View("Edit", product);
            }
            else
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                ShowAdminSidebar();
                return RedirectToAction("Index");
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ShowAdminSidebar();
            var adminProductModelEnum = new AdminManageProductViewModel(product);
            return View(adminProductModelEnum);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            var wl = db.Wishlists.Where(l => l.ProductID.Equals(id));
            db.Wishlists.RemoveRange(wl);
            db.Products.Remove(product);
            db.SaveChanges();
            ShowAdminSidebar();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
