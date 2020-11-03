﻿using System;
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
                ViewBag.YritysSortParam = sortOrder == "OstoMyynti" ? "om_desc" : "OstoMyynti";
                ViewBag.OsakkeetYhtSortParam = sortOrder == "Osakkeiden määrä yhteensä" ? "osakkeetyht_desc" : "Osakkeiden määrä yhteensä";
                ViewBag.HankintaYhtSortParam = sortOrder == "Hankinta-arvo yhteensä" ? "hankintayht_desc" : "Hankinta-arvo yhteensä";
                ViewBag.OsakeNytSortParam = sortOrder == "Osakkeen hinta nyt" ? "osakenyt_desc" : "Osakkeen hinta nyt";
                ViewBag.OmistusNytSortParam = sortOrder == "Omistuksen arvo nyt" ? "omistusnyt_desc" : "Omistuksen arvo nyt";
                ViewBag.VoittoTappioSortParam = sortOrder == "Voitto/Tappio" ? "voittotappio_desc" : "Voitto/Tappio";
                ViewBag.VoittoTappioProsSortParam = sortOrder == "Voitto/Tappio%" ? "voittotappiopros_desc" : "Voitto/Tappio%";

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
                        case "Osakkeen hinta nyt":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderBy(p => p.aHintaNyt);
                            break;
                        case "osakenyt_desc":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderByDescending(p => p.aHintaNyt);
                            break;
                        case "Omistuksen arvo nyt":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderBy(p => p.ArvoNytAll);
                            break;
                        case "omistusnyt_desc":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderByDescending(p => p.ArvoNytAll);
                            break;
                        case "Voitto/Tappio":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderBy(p => p.VoittoTappioE);
                            break;
                        case "voittotappio_desc":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderByDescending(p => p.VoittoTappioE);
                            break;
                        case "Voitto/Tappio%":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderBy(p => p.VoittoTappio_);
                            break;
                        case "voittotappiopros_desc":
                            portfolio = portfolio.Where(p => p.Yritys.Contains(searchString2)).OrderByDescending(p => p.VoittoTappio_);
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
                        case "Osakkeen hinta nyt":
                            portfolio = portfolio.OrderBy(p => p.aHintaNyt);
                            break;
                        case "osakenyt_desc":
                            portfolio = portfolio.OrderByDescending(p => p.aHintaNyt);
                            break;
                        case "Omistuksen arvo nyt":
                            portfolio = portfolio.OrderBy(p => p.ArvoNytAll);
                            break;
                        case "omistusnyt_desc":
                            portfolio = portfolio.OrderByDescending(p => p.ArvoNytAll);
                            break;
                        case "Voitto/Tappio":
                            portfolio = portfolio.OrderBy(p => p.VoittoTappioE);
                            break;
                        case "voittotappio_desc":
                            portfolio = portfolio.OrderByDescending(p => p.VoittoTappioE);
                            break;
                        case "Voitto/Tappio%":
                            portfolio = portfolio.OrderBy(p => p.VoittoTappio_);
                            break;
                        case "voittotappiopros_desc":
                            portfolio = portfolio.OrderByDescending(p => p.VoittoTappio_);
                            break;
                        default:
                            portfolio = portfolio.OrderBy(p => p.Yritys);
                            break;
                    }
                }

                int pageSize = (pagesize ?? 15);
                int pageNumber = (page ?? 1);

                //return View(portfolio.ToPagedList(pageNumber, pageSize));
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

                //Own transactions
                string PvmList;
                string aHintaList;
                //string ID;
                //string OstoMyynti;
                //string Maara;

                List<Transactions> OwnTransactions = new List<Transactions>();

                var ownTransactionsData = from tr in db.Transactions
                                          select tr;

                foreach (Transactions transactions in ownTransactionsData)
                {
                    Transactions oneRow = new Transactions();
                    //oneRow.ID = transactions.ID;
                    oneRow.aHinta = transactions.aHinta;
                    oneRow.Pvm = transactions.Pvm;
                    OwnTransactions.Add(oneRow);
                }

                PvmList = "'" + string.Join("','", OwnTransactions.Select(n => n.Pvm).ToList()) + "'";
                aHintaList = "'" + string.Join("','", OwnTransactions.Select(n => n.aHinta).ToList()) + "'";

                ViewBag.PvmList = PvmList;
                ViewBag.aHintaList = aHintaList;

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
