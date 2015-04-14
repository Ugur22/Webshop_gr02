using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.DatabaseControllers;
using Webshop_gr02.Models;
using MySql.Data.MySqlClient;

namespace Webshop_gr02.Controllers
{
    [Authorize(Roles = "MANAGER")]
    public class ManagerController : Controller
    {
        // GET: /Manager/

        private AuthDBController authDBController = new AuthDBController();

        public ActionResult OmzetMonthly()
        {
            List<Product> producten = authDBController.getTotalOmzet();
            return View(producten);
        }

        [HttpPost]
        public ActionResult OmzetMonthly(string date)
        {
            List<Product> producten = authDBController.getMonthlyOmzet(date);
            return View(producten);
        }

        public ActionResult MeestVerkocht()
        {
            List<Product> producten = authDBController.GetProductTop10();
            return View(producten);
        }

        [HttpPost]
        public ActionResult MeestVerkocht(string date)
        {
            List<Product> producten = authDBController.getMonthlyProductTop10(date);
            return View(producten);
        }

        public ActionResult MinstVerkocht()
        {
            List<Product> producten = authDBController.GetProductBottom10();
            return View(producten);
        }

        [HttpPost]
        public ActionResult MinstVerkocht(string date)
        {
            List<Product> producten = authDBController.GetMonthlyProductBottom10(date);
            return View(producten);
        }
    }
}
