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
    
    public class ManagerController : Controller
    {
        // GET: /Manager/
		
        private AuthDBController authDBController = new AuthDBController();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManagerHomepage()
        {
            return View();
        }

        public ActionResult OmzetMonthly()
        {
            List<Product> producten = authDBController.getTotalOmzet();

            foreach(Product p in producten){
                Console.WriteLine(p);
            }

            return View(producten);
        }

        public ActionResult MeestVerkocht()
        {
            List<Product> producten = authDBController.GetProductTop10();

            foreach (Product p in producten)
            {
                Console.WriteLine(p);
            }

            return View(producten);
        }

        public ActionResult MinstVerkocht()
        {
            List<Product> producten = authDBController.GetProductBottom10();

            foreach (Product p in producten)
            {
                Console.WriteLine(p);
            }

            return View(producten);
        }
		
        [HttpPost]
        public ActionResult OmzetMonthly(string date)
        {
            Console.WriteLine(date);
            
            List<Product> producten = authDBController.getMonthlyOmzet(date);
            
            return View(producten);
        }
    }
}
