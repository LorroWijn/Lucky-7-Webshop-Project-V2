﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHDomtica.Models;
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
            return View(db.User.ToList());
        }

        //Code for the AdminsideBar
        private void ShowAdminSidebar()
        {
            System.Diagnostics.Debug.WriteLine($"AdminSidebar {Request.RawUrl}");
            ViewBag.ShowAdminSideBar = true;
        }

        // GET: AdminManageUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ShowAdminSidebar();
            return View(user);
        }

        // GET: AdminManageUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminManageUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Gender,EMail,Password,ConfirmPassword,Country,Province,City,ZipCode,BillingAddress")] User user)
        {
            user.Password = Crypto.Hash(user.Password);
            user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
            if (db.User.Any(x => x.EMail == user.EMail))
            {
                ViewBag.DuplicateMessage = "E-mail is al in gebruik. Probeer een ander E-mailadres";
                ShowAdminSidebar();
                return View("Create", user);
            }
            else
            {
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        // GET: AdminManageUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ShowAdminSidebar();
            return View(user);
        }

        // POST: AdminManageUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Gender,EMail,Password,ConfirmPassword,Country,Province,City,ZipCode,BillingAddress")] User user)
        {
            user.Password = Crypto.Hash(user.Password);
            user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                ShowAdminSidebar();
                return RedirectToAction("Index");
            }
            ShowAdminSidebar();
            return View(user);
        }

        // GET: AdminManageUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ShowAdminSidebar();
            return View(user);
        }

        // POST: AdminManageUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.User.Find(id);
            db.User.Remove(user);
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