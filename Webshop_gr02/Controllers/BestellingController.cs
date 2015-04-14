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

        public ActionResult Bestelling()
        {

            bool goldmember = false;
            string welOfNiet = "";

            try
            {
                Bestelling bestelling = authDBController.GetBestelling(1);
                //List<BestelRegel> bestelRegels = authDBController.GetBestellingOverzicht();
                goldmember = authDBController.ControleerGoldMember();
                if (goldmember == true)
                {
                    welOfNiet = "Je bent GoldMember";

                }
                else
                {
                    welOfNiet = "Je bent geen GoldMember";
                }
                ViewBag.goldmembership = welOfNiet;
                return View(bestelling);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }


        }

        //public ViewResult Bestelling()
        //{

        //    bool goldmember = false;
        //    string welOfNiet = "";

        //    try
        //    {

        //        goldmember = authDBController.ControleerGoldMember();
        //        if (goldmember == true)
        //        {
        //            welOfNiet = "Je bent GoldMember";

        //        }
        //        else {
        //            welOfNiet = "Je bent geen GoldMember";
        //        }
        //        ViewBag.goldmembership = welOfNiet;
        //        return View();
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
        //        return View();
        //    }

        //}


        public ActionResult ProductBestel(int id, float bedrag, int voorraad) {

            Console.WriteLine(id);
          int aantal = 1;

          voorraad = voorraad - aantal;
        authDBController.BestelProduct(id, aantal, bedrag);
        authDBController.WijzigVoorraad(id, voorraad);

        return RedirectToAction("BestellingGelukt", "Bestelling");


        }




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
            List<Bestelling> bestelling = authDBController.GetAllOrderedProducts();

            foreach (Bestelling BR in bestelling)
            {
                Console.WriteLine(BR);
            }

            return View(bestelling);
        }

        public ActionResult BestellingGelukt() {

            return View();
        }

        [HttpPost]
        public ActionResult WijzigBesteldeProducten(Bestelling bestelling)
        {
            Console.WriteLine(bestelling);
            try
            {
                authDBController.UpdateOrderedProducts(bestelling);

            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;
            }
            return RedirectToAction("OverzichtBesteldeProducten", "Bestelling");
        }

       

    }
}
