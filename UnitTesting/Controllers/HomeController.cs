using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnitTesting.DataAccessLayer;
using UnitTesting.Models;
using UnitTesting.Repository;

namespace UnitTesting.Views.Home
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
        public ActionResult Index()
        {
            var clients = GRepo.GetClients();
            return View(clients);
        }

        // GET: Accounts
        public ActionResult Accounts(int? id)
        {
            if(id != null)
            {
                var accounts = GRepo.GetAccounts((int)id);
                ViewBag.id = id;
                return View(accounts.ToList());
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
        public ActionResult CreateClient([Bind(Include = "Id,Balance,ClientId")] Client client)
        {
            if (ModelState.IsValid)
            {
                GRepo.AddClient(client);
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Accounts/Create
        public ActionResult CreateAccount(int? id)
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
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = GRepo.GetAccount((int)id);
            GRepo.RemoveAccount(account);
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
    }
}
