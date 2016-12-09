using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankSystem.DataAccessLayer;
using BankSystem.Models;
using BankSystem.Repository;
using BankSystem.ViewModels;

namespace BankSystem.Controllers
{
    public class HomeController : Controller
    {
        private BankRepo GRepo { get; set; }

        public HomeController() {
            this.GRepo = new WorkingBankRepo();
        }

        public HomeController(BankRepo grepo)
        {
            this.GRepo = grepo;
        }

        // GET: Clients
        public ViewResult Index()
        {
            var clients = GRepo.GetClients();
            return View(clients);
        }

        // GET: Accounts
        public ActionResult Accounts(int? id)
        {
            if(id != null)
            {
                var client = GRepo.GetClient((int)id);
                return View(client);
            }
            return RedirectToAction("Index");
            
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = GRepo.GetAccount((int)id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult CreateClient()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClient([Bind(Include = "Id,Name")] Client client)
        {
            if (ModelState.IsValid)
            {
                GRepo.AddClient(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Accounts/Create
        public RedirectToRouteResult CreateAccount(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Account account = new Account(){ ClientId = (int)id};
            GRepo.AddAccount(account);
            return RedirectToAction("Accounts", new { id = id });
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = GRepo.GetAccount((int)id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(GRepo.GetClients(), "Id", "Name", account.ClientId);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Balance,ClientId")] Account account)
        {
            if (ModelState.IsValid)
            {
                GRepo.EditAccount(account);
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(GRepo.GetClients(), "Id", "Name", account.ClientId);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = GRepo.GetAccount((int)id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = GRepo.GetClientId((int)id);
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = GRepo.GetAccount((int)id);
            GRepo.RemoveAccount(account);
            return RedirectToAction("Accounts", new { id = GRepo.GetClientId((int)id) });
        }

        public ActionResult Deposit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            ViewBag.AccountId = id;
            ViewBag.ClientId = GRepo.GetClientId((int)id);
            return View();
        }

        [HttpPost, ActionName("Deposit")]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit([Bind(Include = "Id,Amount,AccountId")] Deposit deposit)
        {
            if(ModelState.IsValid)
            {
                GRepo.Deposit(deposit);
            }
            ViewBag.ClientId = GRepo.GetClientId(deposit);
            return RedirectToAction("Accounts", new { id = GRepo.GetClientId(deposit) });
        }

        public ActionResult Withdraw(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            if(GRepo.GetAccount((int)id).isLocked)
                return RedirectToAction("Accounts", new { id = GRepo.GetClientId((int)id) });

            ViewBag.AccountId = id;
            ViewBag.ClientId = GRepo.GetClientId((int)id);
            return View();
        }

        [HttpPost, ActionName("Withdraw")]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw([Bind(Include = "Id,Amount,AccountId")] Withdraw withdraw)
        {
            if (ModelState.IsValid)
            {
                GRepo.Withdraw(withdraw);
            }
            ViewBag.ClientId = GRepo.GetClientId(withdraw);
            return RedirectToAction("Accounts", new { id = GRepo.GetClientId(withdraw) });
        }

        public ActionResult Transaction(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            ViewBag.FromId = id;
            ViewBag.ClientId = GRepo.GetClientId((int)id);
            return View();
        }

        [HttpPost, ActionName("Transaction")]
        [ValidateAntiForgeryToken]
        public ActionResult Transaction([Bind(Include = "Id,Amount,FromId,ToId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                GRepo.Transaction(transaction);
            }
            ViewBag.ClientId = GRepo.GetClientId(transaction);
            return RedirectToAction("Accounts", new { id = GRepo.GetClientId(transaction) });
        }

        public ActionResult History(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            HistoryModel history = GRepo.History((int)id);
            ViewBag.ClientId = GRepo.GetClientId((int)id);
            return View(history);
        }

        public ActionResult DeleteClient(int? id)
        {
            if (id != null)
                GRepo.RemoveClient((int)id);

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Lock(int? id)
        {
            if (id != null)
                GRepo.LockUnlockAccount((int)id);

            return RedirectToAction("Accounts", new { id = GRepo.GetClientId((int)id) });
        }
    }
}
