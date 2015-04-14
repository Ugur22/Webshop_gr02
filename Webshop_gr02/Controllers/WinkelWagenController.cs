using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.DatabaseControllers;
using Webshop_gr02.Models;

namespace Webshop_gr02.Controllers
{
    public class WinkelWagenController : Controller
    {
        //
        // GET: /WinkelWagen/

        private AuthDBController authDBController = new AuthDBController();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult OverzichtWinkelWagen()
        {

            string username = User.Identity.Name;
            try
            {
                List<BestelRegel> BestelRegel = authDBController.GetBestellinglijst(username);
                return View(BestelRegel);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }


        }

    }
}
