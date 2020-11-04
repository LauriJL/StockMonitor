using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using StockMonitor_2.Models;
using StockMonitor_2.ViewModels;

namespace StockMonitor_2.Controllers
{
    public class PortfolioController : Controller
    {
        private StockMonitorEntities11 db = new StockMonitorEntities11();

        // GET: Portfolio
        public ActionResult Index(string searchString2, string sortOrder, string currentFilter1, int? page, int? pagesize)
        {
            //Session control
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.YritysSortParam = string.IsNullOrEmpty(sortOrder) ? "yritys_desc" : "";
                ViewBag.OsakkeetYhtSortParam = sortOrder == "Osakkeiden määrä yhteensä" ? "osakkeetyht_desc" : "Osakkeiden määrä yhteensä";
                ViewBag.HankintaYhtSortParam = sortOrder == "Hankinta-arvo yhteensä" ? "hankintayht_desc" : "Hankinta-arvo yhteensä";

                if (searchString2 != null)
                {
                    page = 1;
                }
                else
                {
                    searchString2 = currentFilter1;
                }
                ViewBag.currentFilter1 = searchString2;

                StockMonitorEntities11 db = new StockMonitorEntities11();

                //Display only db items belonging to session user
                var userId = Session["UserName"];
                var portfolio = from p in db.Portfolio
                                select p;

                if (!String.IsNullOrEmpty(searchString2))
                {
                    switch (sortOrder)
                    {
                        case "yritys_desc":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderByDescending(p => p.Yritys);
                            break;
                        case "Osakkeiden määrä yhteensä":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderBy(p => p.MaaraYht);
                            break;
                        case "osakkeetyht_desc":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderByDescending(p => p.MaaraYht);
                            break;
                        case "Hankinta-arvo yhteensä":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderBy(p => p.HankintaArvo);
                            break;
                        case "hankintayht_desc":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderByDescending(p => p.HankintaArvo);
                            break;
                        default:
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderBy(p => p.Yritys);
                            break;
                    }
                }
                else
                {
                    switch (sortOrder)
                    {
                        case "yritys_desc":
                            portfolio = portfolio.OrderByDescending(p => p.Yritys);
                            break;
                        case "Osakkeiden määrä yhteensä":
                            portfolio = portfolio.OrderBy(p => p.MaaraYht);
                            break;
                        case "osakkeetyht_desc":
                            portfolio = portfolio.OrderByDescending(p => p.MaaraYht);
                            break;
                        case "Hankinta-arvo yhteensä":
                            portfolio = portfolio.OrderBy(p => p.HankintaArvo);
                            break;
                        case "hankintayht_desc":
                            portfolio = portfolio.OrderByDescending(p => p.HankintaArvo);
                            break;
                        default:
                            portfolio = portfolio.OrderBy(p => p.Yritys);
                            break;
                    }
                }

                int pageSize = (pagesize ?? 15);
                int pageNumber = (page ?? 1);

                return View(portfolio.Where(t => t.Kayttaja == userId).ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Portfolio/DetailsYahoo/
        public ActionResult Details(int? id)
        {
            //Session control
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Portfolio portfolio = db.Portfolio.Find(id);
                if (portfolio == null)
                {
                    return HttpNotFound();
                }
                return View(portfolio);
            }
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
