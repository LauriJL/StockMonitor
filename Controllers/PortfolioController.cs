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

            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);

            return View(portfolio.ToPagedList(pageNumber, pageSize));

            //return View(portfolio);
        }

        // GET: Portfolio/Details/5
        public ActionResult Details(int? id)
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

        // GET: Portfolio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Portfolio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Kayttaja,Yritys,MaaraYht,HankintaArvo,aHintaNyt,ArvoNytAll,VoittoTappioE,VoittoTappio_")] Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                db.Portfolio.Add(portfolio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(portfolio);
        }

        // GET: Portfolio/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Portfolio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Kayttaja,Yritys,MaaraYht,HankintaArvo,aHintaNyt,ArvoNytAll,VoittoTappioE,VoittoTappio_")] Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portfolio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(portfolio);
        }

        // GET: Portfolio/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Portfolio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Portfolio portfolio = db.Portfolio.Find(id);
            db.Portfolio.Remove(portfolio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index2()
        {
            StockMonitorEntities11 db = new StockMonitorEntities11();

            var portfolio = from p in db.Portfolio
                            select p;

            return View(portfolio);
        }

        public ActionResult _PortfolioTransactions(string yritys)
        {
            var PortTrans = from t in db.Transactions
                            join p in db.Portfolio on t.Yritys equals p.Yritys
                            join c in db.Company on t.Yritys equals c.Symbol
                            where p.Yritys == yritys
                            select new TransactionsPortfolio
                            {
                                Yritys = t.Yritys,
                                Pvm = t.Pvm,
                                OstoMyynti = t.OstoMyynti,
                                Maara = t.Maara,
                                aHinta = t.aHinta,
                                Total = (decimal)t.Total,
                                Valuutta = t.Valuutta,
                                Kurssi = (decimal)t.Kurssi,
                                TotalEuros = (decimal)t.TotalEuros,
                                Kulut = t.Kulut,
                                Grandtotal = (decimal)t.Grandtotal
                            };

            return PartialView(PortTrans);
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
