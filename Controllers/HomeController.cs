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

        [HttpPost]
        public ActionResult Authorize(Users LoginModel)
        {
            StockEntities1 db = new StockEntities1();

            var LoggedUser = db.Users.SingleOrDefault(x => x.KayttajaNimi == LoginModel.KayttajaNimi && x.Salasana == LoginModel.Salasana);
            if (LoggedUser != null)
            {
                if (LoggedUser.Rooli.Equals("SuperUser"))
                {
                    ViewBag.LoginMessage = "Successful login";
                    ViewBag.LoggedStatus = "In";
                    ViewBag.LoginError = 0;
                    Session["UserName"] = LoggedUser.KayttajaNimi;
                    Session["LoginID"] = LoggedUser.Salasana;
                    Session["Role"] = LoggedUser.Rooli;
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    ViewBag.LoginMessage = "Successful login";
                    ViewBag.LoggedStatus = "In";
                    ViewBag.LoginError = 0;
                    Session["UserName"] = LoggedUser.KayttajaNimi;
                    Session["LoginID"] = LoggedUser.Salasana;
                    Session["Role"] = LoggedUser.Rooli;
                    return RedirectToAction("Index", "Portfolio");
                }              
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