using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StockMonitor_2.Models;
using PagedList;

namespace StockMonitor_2.Controllers
{
    public class TransactionsController : Controller
    {
        private StockMonitorEntities11 db = new StockMonitorEntities11();

        // GET: Transactions
        public ActionResult Index(string searchString1, string sortOrder, string currentFilter, int? page, int? pagesize)
        {
            //Session control
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else { 
                ViewBag.CurrentSort = sortOrder;
                ViewBag.PvmSortParam = sortOrder == "Pvm" ? "pvm_desc" : "Pvm";
                ViewBag.OstoMyyntiSortParam = sortOrder == "OstoMyynti" ? "om_desc" : "OstoMyynti";
                ViewBag.YritysSortParam = string.IsNullOrEmpty(sortOrder) ? "yritys_desc" : "";
                ViewBag.MaaraSortParam = sortOrder == "Maara" ? "maara_desc" : "Maara";
                ViewBag.aHintaSortParam = sortOrder == "aHinta" ? "ahinta_desc" : "aHinta";
                ViewBag.SummaSortParam = sortOrder == "Summa" ? "summa_desc" : "Summa";
                ViewBag.ValuuttaSortParam = sortOrder == "Valuutta" ? "valuutta_desc" : "Valuutta";
                ViewBag.KurssiSortParam = sortOrder == "Kurssi" ? "kurssi_desc" : "Kurssi";
                ViewBag.SummaESortParam = sortOrder == "Summa (€)" ? "summaE_desc" : "Summa (€)";
                ViewBag.KulutSortParam = sortOrder == "Kulut" ? "kulut_desc" : "Kulut";
                ViewBag.GrandtotalSortParam = sortOrder == "Kaupan loppusumma (€)" ? "grandtotal_desc" : "Kaupan loppusumma (€)";

                if (searchString1 != null)
                {
                    page = 1;
                }
                else
                {
                    searchString1 = currentFilter;
                }

                ViewBag.currentFilter = searchString1;

                StockMonitorEntities11 db = new StockMonitorEntities11();
            
                //Display only db items belonging to session user
                var userId = Session["UserName"];
                var transactions = from t in db.Transactions.Include(t => t.Currency).Include(t => t.Users).Include(t => t.TransactionTypes)
                                   select t;

                if (!String.IsNullOrEmpty(searchString1))
                {
                    switch (sortOrder)
                    {
                        case "pvm_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.Pvm);
                            break;
                        case "Pvm":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.Pvm);
                            break;
                        case "om_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.OstoMyynti);
                            break;
                        case "OstoMyynti":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.OstoMyynti);
                            break;
                        case "yritys_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.Yritys);
                            break;
                        case "Maara":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.Maara);
                            break;
                        case "maara_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.Maara);
                            break;
                        case "aHinta":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.aHinta);
                            break;
                        case "ahinta_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.aHinta);
                            break;
                        case "Summa":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.Total);
                            break;
                        case "summa_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.Total);
                            break;
                        case "Valuutta":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.Currency.Currency1);
                            break;
                        case "valuutta_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.Currency.Currency1);
                            break;
                        case "Kurssi":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.Kurssi);
                            break;
                        case "kurssi_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.Kurssi);
                            break;
                        case "Summa (€)":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.TotalEuros);
                            break;
                        case "summaE_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.TotalEuros);
                            break;
                        case "Kulut":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.Kulut);
                            break;
                        case "kulut_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.Kulut);
                            break;
                        case "Kaupan loppusumma (€)":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.Grandtotal);
                            break;
                        case "grandtotal_desc":
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderByDescending(t => t.Grandtotal);
                            break;
                        default:
                            transactions = transactions.Where(t => t.Yritys.Contains(searchString1)).OrderBy(t => t.Yritys);
                            break;
                    }
                }
                else
                {
                    switch (sortOrder)
                    {
                        case "pvm_desc":
                            transactions = transactions.OrderByDescending(t => t.Pvm);
                            break;
                        case "Pvm":
                            transactions = transactions.OrderBy(t => t.Pvm);
                            break;
                        case "om_desc":
                            transactions = transactions.OrderByDescending(t => t.OstoMyynti);
                            break;
                        case "OstoMyynti":
                            transactions = transactions.OrderBy(t => t.OstoMyynti);
                            break;
                        case "yritys_desc":
                            transactions = transactions.OrderByDescending(t => t.Yritys);
                            break;
                        case "Maara":
                            transactions = transactions.OrderBy(t => t.Maara);
                            break;
                        case "maara_desc":
                            transactions = transactions.OrderByDescending(t => t.Maara);
                            break;
                        case "aHinta":
                            transactions = transactions.OrderBy(t => t.aHinta);
                            break;
                        case "ahinta_desc":
                            transactions = transactions.OrderByDescending(t => t.aHinta);
                            break;
                        case "Summa":
                            transactions = transactions.OrderBy(t => t.Total);
                            break;
                        case "summa_desc":
                            transactions = transactions.OrderByDescending(t => t.Total);
                            break;
                        case "Valuutta":
                            transactions = transactions.OrderBy(t => t.Currency.Currency1);
                            break;
                        case "valuutta_desc":
                            transactions = transactions.OrderByDescending(t => t.Currency.Currency1);
                            break;
                        case "Kurssi":
                            transactions = transactions.OrderBy(t => t.Kurssi);
                            break;
                        case "kurssi_desc":
                            transactions = transactions.OrderByDescending(t => t.Kurssi);
                            break;
                        case "Summa (€)":
                            transactions = transactions.OrderBy(t => t.TotalEuros);
                            break;
                        case "summaE_desc":
                            transactions = transactions.OrderByDescending(t => t.TotalEuros);
                            break;
                        case "Kulut":
                            transactions = transactions.OrderBy(t => t.Kulut);
                            break;
                        case "kulut_desc":
                            transactions = transactions.OrderByDescending(t => t.Kulut);
                            break;
                        case "Kaupan loppusumma (€)":
                            transactions = transactions.OrderBy(t => t.Grandtotal);
                            break;
                        case "grandtotal_desc":
                            transactions = transactions.OrderByDescending(t => t.Grandtotal);
                            break;
                        default:
                            transactions = transactions.OrderBy(t => t.Yritys);
                            break;
                    }
                }            

                int pageSize = (pagesize ?? 10);
                int pageNumber = (page ?? 1);

                //return View(transactions.ToPagedList(pageNumber, pageSize));
                return View(transactions.Where(t => t.Kayttaja == userId).ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: Transactions/Details/5
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
                Transactions transactions = db.Transactions.Find(id);
                if (transactions == null)
                {
                    return HttpNotFound();
                }
                return View(transactions);
            }
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {   
            //Session control
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //Ensure addition is added to session userID in database
                List<SelectListItem> kayttajalista = new List<SelectListItem>();
                {
                    kayttajalista.Add(new SelectListItem
                    {
                        Value = Session["UserName"].ToString(),
                        Text = Session["UserName"].ToString()
                    });
                }

                ViewBag.Kayttaja = new SelectList(kayttajalista, "Value", "Text", null);
                ViewBag.Valuutta = new SelectList(db.Currency, "Currency1", "Currency1");
                ViewBag.OstoMyynti = new SelectList(db.TransactionTypes, "Type", "Type");
                return View();
            }
        }

        // POST: Transactions/Create
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

        public ActionResult _TransactionModalEdit(int? id)
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
        }

        // POST: Transactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _TransactionModalEdit([Bind(Include = "ID,Kayttaja,OstoMyynti,Pvm,Yritys,Maara,MaaraForPortfolio,aHinta,Total,TotalForPortfolio,Valuutta,Kurssi,TotalEuros,Kulut,Grandtotal")] Transactions transactions)
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
            //return View(transactions);
            return PartialView("_TransactionModalEdit", transactions);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
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
        }

        // POST: Transactions/Edit/5
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
            //return View(transactions);
            return PartialView("_TransactionModalEdit", transactions);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
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
                Transactions transactions = db.Transactions.Find(id);
                if (transactions == null)
                {
                    return HttpNotFound();
                }
                return View(transactions);
            }
        }

        public ActionResult _TransactionModalDelete(int? id)
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
                Transactions transactions = db.Transactions.Find(id);
                if (transactions == null)
                {
                    return HttpNotFound();
                }
                return View(transactions);
            }
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("_TransactionModalDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            db.Transactions.Remove(transactions);
            db.SaveChanges();
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
