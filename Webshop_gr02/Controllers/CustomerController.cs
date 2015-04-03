using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;

namespace Webshop_gr02.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        private AuthDBController authDBController = new AuthDBController();

        public ActionResult ProductenOverzichtklant()
        {
            try
            {
                List<Product> productenLijst = authDBController.GetProductLijst();
                return View(productenLijst);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

    }
}
