using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;
using Webshop_gr02.ViewModels;

namespace Webshop_gr02.Controllers
{
    public class BestellingController : Controller
    {
        private AuthDBController authDBController = new AuthDBController();
        
        public ActionResult OverzichtBesteldeProducten()
        {
            try
            {
                List<Bestelling> bestelling = authDBController.GetAllOrderedProducts();
                return View(bestelling);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

        public ActionResult WijzigBesteldeProducten(int BestellingID)
        {
            authDBController.setBetaald(BestellingID);

            return RedirectToAction("OverzichtBesteldeProducten");
        }

        public ActionResult BestellingGelukt()
        {
            return View();
        }

        //public ActionResult OverzichtBestellingKlant()
        //{

        //    bool goldmember = false;
        //    string welOfNiet = "";

        //    try
        //    {
        //        Bestelling bestelling = authDBController.GetBestelling(1);
        //        List<BestelRegel> bestelRegels = authDBController.GetBestellingOverzicht();
        //        goldmember = authDBController.ControleerGoldMember();
        //        if (goldmember == true)
        //        {
        //            welOfNiet = "Je bent GoldMember";

        //        }
        //        else
        //        {
        //            welOfNiet = "Je bent geen GoldMember";
        //        }
        //        ViewBag.goldmembership = welOfNiet;
        //        return View(bestelling);
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
        //        return View();
        //    }


        //}


        public ActionResult OverzichtBestellingKlant()
        {
            try
            {
                BestelRegel besteldeProducten = authDBController.GetBestellingOverzichtKlant();
                return View(besteldeProducten);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

    }
}
