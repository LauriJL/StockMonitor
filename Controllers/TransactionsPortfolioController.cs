using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockMonitor_2.Models;
using StockMonitor_2.ViewModels;

namespace StockMonitor_2.Controllers
{
    public class TransactionsPortfolioController : Controller
    {
        private StockMonitorEntities11 db = new StockMonitorEntities11();

        // GET: TransactionsPortfolio
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Currency).Include(t => t.Users).Include(t => t.TransactionTypes);
            return View(transactions.ToList());
        }

        // GET: TransactionsPortfolio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // GET: TransactionsPortfolio/Create
        public ActionResult Create()
        {
            ViewBag.Valuutta = new SelectList(db.Currency, "Currency1", "Currency1");
            ViewBag.Kayttaja = new SelectList(db.Users, "KayttajaNimi", "Rooli");
            ViewBag.OstoMyynti = new SelectList(db.TransactionTypes, "Type", "Type");
            return View();
        }

        // POST: TransactionsPortfolio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Kayttaja,OstoMyynti,Pvm,Yritys,Maara,MaaraForPortfolio,aHinta,Total,TotalForPortfolio,Valuutta,Kurssi,TotalEuros,Kulut,Grandtotal")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transactions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Valuutta = new SelectList(db.Currency, "Currency1", "Currency1", transactions.Valuutta);
            ViewBag.Kayttaja = new SelectList(db.Users, "KayttajaNimi", "Rooli", transactions.Kayttaja);
            ViewBag.OstoMyynti = new SelectList(db.TransactionTypes, "Type", "Type", transactions.OstoMyynti);
            return View(transactions);
        }

        // GET: TransactionsPortfolio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            ViewBag.Valuutta = new SelectList(db.Currency, "Currency1", "Currency1", transactions.Valuutta);
            ViewBag.Kayttaja = new SelectList(db.Users, "KayttajaNimi", "Rooli", transactions.Kayttaja);
            ViewBag.OstoMyynti = new SelectList(db.TransactionTypes, "Type", "Type", transactions.OstoMyynti);
            return View(transactions);
        }

        // POST: TransactionsPortfolio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Kayttaja,OstoMyynti,Pvm,Yritys,Maara,MaaraForPortfolio,aHinta,Total,TotalForPortfolio,Valuutta,Kurssi,TotalEuros,Kulut,Grandtotal")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Valuutta = new SelectList(db.Currency, "Currency1", "Currency1", transactions.Valuutta);
            ViewBag.Kayttaja = new SelectList(db.Users, "KayttajaNimi", "Rooli", transactions.Kayttaja);
            ViewBag.OstoMyynti = new SelectList(db.TransactionTypes, "Type", "Type", transactions.OstoMyynti);
            return View(transactions);
        }

        // GET: TransactionsPortfolio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // POST: TransactionsPortfolio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            db.Transactions.Remove(transactions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PortfolioTransactions(string company)
        {
            var PortTrans = from t in db.Transactions
                            join p in db.Portfolio on t.Yritys equals p.Yritys
                            where p.Yritys.Contains("AAPL")
                            //where p.Yritys == company
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

            return View(PortTrans);
        }

        public ActionResult _PortfolioTransactions()
        {
            var PortTrans = from t in db.Transactions
                            join p in db.Portfolio on t.Yritys equals p.Yritys
                            join c in db.Company on t.Yritys equals c.Symbol
                            //where p.Yritys.Contains("NVDA")
                            select new TransactionsPortfolio
                            {
                                ID = c.ID,
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
