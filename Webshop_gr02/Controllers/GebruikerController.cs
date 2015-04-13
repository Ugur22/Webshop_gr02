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

        [Authorize(Roles = "BEHEERDER")]
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

        [Authorize(Roles = "BEHEERDER")]
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

        [Authorize(Roles = "BEHEERDER")]
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



        [Authorize(Roles = "KLANT")]
        public ActionResult UserOverzicht(string username)
        {
            Gebruiker gebruiker = authDBController.getGebruikerGegevens(username);
            return View(gebruiker);
        }



        [Authorize(Roles = "KLANT")]
        public ActionResult wijzigenKlantGegevens(string username)
        {
            Gebruiker gebruiker = authDBController.getGebruikerGegevens(username);
            return View(gebruiker);
        }



        [Authorize(Roles = "KLANT")]
        [HttpPost]
        public ActionResult wijzigenKlantGegevens(Gebruiker gebruiker, int ID)
        {
            if (ModelState.IsValid)
            {

                authDBController.updateGebruikerGegevens(gebruiker, ID);
                return RedirectToAction("UserOverzicht", new { username = gebruiker.Username });


            }
            else
            {
                return View(gebruiker);

            }
        }
    }
}
