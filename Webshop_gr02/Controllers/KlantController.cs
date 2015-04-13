using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.DatabaseControllers;
using Webshop_gr02.Models;

namespace Webshop_gr02.Controllers
{
    public class KlantController : Controller
    {
        //
        // GET: /Klant/
        private AuthDBController authDBController = new AuthDBController();

        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult OverzichtKlant()
        //{
        //    try
        //    {
        //        List<Klant> klant = authDBController.GetKlant();
        //        return View(klant);


        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
        //        return View();
        //    }


        //}

        //[Authorize(Roles = "KLANT")]
        //public ActionResult OverzichtKlant(string username)
        //{
        //    Klant klant = authDBController.getKlantGegevens(username);

        //    return View(klant);
        //}

        

    }
}
