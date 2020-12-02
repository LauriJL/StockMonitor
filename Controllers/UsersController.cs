using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockMonitor_2.Models;

namespace StockMonitor_2.Views
{
    public class UsersController : Controller
    {
        private StockMonitorEntities11 db = new StockMonitorEntities11();

        // GET: Users
        public ActionResult Index()
        {
            //Session control
            if (!Session["Role"].Equals("Pääkäyttäjä"))
            {
                return RedirectToAction("AccessDenied", "Users");
            }
            else
            {
                var users = db.Users.Include(u => u.UserRoles);
                return View(users.ToList());
            }
        }

        public ActionResult ViewProfile(string kayttaja)
        {
            if (Session["UserName"] != null)
            {
                kayttaja = Convert.ToString(Session["UserName"]);
                var userDetails = db.Users.Where(x => x.KayttajaNimi == kayttaja);
                return View(userDetails);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            //Session control
            if (!Session["Role"].Equals("Pääkäyttäjä"))
            {
                return RedirectToAction("AccessDenied", "Users");
            }
            else
            {
                ViewBag.Rooli = new SelectList(db.UserRoles, "Role", "Role");
                return View();
            }
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KayttajaNimi,Rooli,Etunimi,Sukunimi,Salasana,Sahkoposti")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Rooli = new SelectList(db.UserRoles, "Role", "Role", users.Rooli);
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            //Session control
            if (!Session["Role"].Equals("Pääkäyttäjä"))
            {
                return RedirectToAction("AccessDenied", "Users");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Rooli = new SelectList(db.UserRoles, "Role", "Role", users.Rooli);
                return View(users);
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KayttajaNimi,Rooli,Etunimi,Sukunimi,Salasana,Sahkoposti")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Rooli = new SelectList(db.UserRoles, "Role", "Role", users.Rooli);
            return View(users);
        }

        // GET: Users/EditOwnInfo
        public ActionResult EditOwn(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.Rooli = new SelectList(db.UserRoles, "Role", "Role", users.Rooli);
            return View(users);
        }

        // POST: Users/EditOwnInfo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOwn([Bind(Include = "KayttajaNimi,Rooli,Etunimi,Sukunimi,Salasana,Sahkoposti")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserDataEdited");
            }
            ViewBag.Rooli = new SelectList(db.UserRoles, "Role", "Role", users.Rooli);
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            //Session control
            if (!Session["Role"].Equals("Pääkäyttäjä"))
            {
                return RedirectToAction("AccessDenied", "Users");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Users users = db.Users.Find(id);
                if (users == null)
                {
                    return HttpNotFound();
                }
                return View(users);
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UserDataEdited()
        {
            return View();
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
