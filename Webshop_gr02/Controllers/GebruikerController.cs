using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.DatabaseControllers;
using Webshop_gr02.Models;

namespace Webshop_gr02.Controllers
{
    public class GebruikerController : Controller
    {
        //
        // GET: /Gebruiker/
        private AuthDBController authDBController = new AuthDBController();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult OverzichtGebruiker()
        {
            try
            {
                List<Gebruiker> gebruiker = authDBController.GetGebruiker();
                return View(gebruiker);


            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }


        }

        public ActionResult WijzigGebruiker(int ID_G)
        {
            try
            {

                Gebruiker gebruiker = authDBController.Getgebruiker(ID_G);
                return View(gebruiker);
            }
            catch (Exception e)
            {
                ViewBag.FoutMelding = "Er is iets fout gegaan: " + e;
                return View();
            }
        }


        [HttpPost]
        public ActionResult WijzigGebruiker(Gebruiker gebruiker)
        {
            try
            {
                authDBController.UpdateGebruiker(gebruiker);

            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;
            }
            return RedirectToAction("OverzichtGebruiker", "Gebruiker");
        }
        //public ActionResult WijzigAanbieding()
        //{
        //    return View();
        //}


        public ActionResult ToevoegenAanbieding()
        {
            return View();
        }



    }
}
