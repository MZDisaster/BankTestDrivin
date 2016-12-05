using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitTesting.Repository;

namespace UnitTesting.Controllers
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

        public ViewResult Index()
        {
            return View(GRepo.GetClients());
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