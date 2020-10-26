using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockMonitor_2.Models;

namespace StockMonitor_2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.LoginError = 0;
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult _LoginForm()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Authorize(Users LoginModel)
        {
            StockMonitorEntities11 db = new StockMonitorEntities11();

            var LoggedUser = db.Users.SingleOrDefault(x => x.KayttajaNimi == LoginModel.KayttajaNimi && x.Salasana == LoginModel.Salasana);
            if (LoggedUser != null)
            {
                ViewBag.LoginMessage = "Successfull login";
                ViewBag.LoggedStatus = "In";
                ViewBag.LoginError = 0;
                Session["UserName"] = LoggedUser.KayttajaNimi;
                Session["LoginID"] = LoggedUser.Salasana;
                return RedirectToAction("Index", "Portfolio");
            }
            else
            {
                ViewBag.LoginMessage = "Login unsuccessfull";
                ViewBag.LoggedStatus = "Out";
                ViewBag.LoginError = 1;
                LoginModel.LoginErrorMessage = "Tuntematon käyttäjätunnus tai salasana.";
                return View("Index", LoginModel);
            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            ViewBag.LoggedStatus = "Out";
            return RedirectToAction("Index", "Home");
        }

    }
}