using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;
namespace Webshop_gr02.Controllers
{
    public class AanbiedingController : Controller
    {
        //
        // GET: /Aanbieding/

        private AuthDBController authDBController = new AuthDBController();

        public ActionResult OverzichtAanbiedingen()
        {
            try
            {
                List<Aanbieding> aanbiedingen = authDBController.GetAanbiedingen();
                return View(aanbiedingen);


            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }


        }

        private SelectList GetAanbiedingen()
        {
            List<Aanbieding> aanbieding = authDBController.GetAanbiedingen();
            Aanbieding emptyaanbieding = new Aanbieding { ID_A = 0, soort = "" };
            aanbieding.Insert(0, emptyaanbieding);

            return new SelectList(aanbieding, "ID_A", "soort");
        }

        public ActionResult WijzigAanbieding(int aanbiedingId)
        {
            try
            {

                Aanbieding aanbieding = authDBController.GetAAnbieding(aanbiedingId);
                return View(aanbieding);
            }
            catch (Exception e)
            {
                ViewBag.FoutMelding = "Er is iets fout gegaan: " + e;
                return View();
            }
        }


        [HttpPost]
        public ActionResult WijzigAanbieding(Aanbieding aanbieding)
        {
            Console.WriteLine(aanbieding);
            try
            {
                if (ModelState.IsValid)
                {
                    authDBController.UpdateAanbieding(aanbieding);
                    return RedirectToAction("OverzichtAanbiedingen", "Aanbieding");
                }
                else
                {
                    return View();
                }


            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;
                return View();
            }

        }
        //public ActionResult WijzigAanbieding()
        //{
        //    return View();
        //}


        public ActionResult ToevoegenAanbieding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ToevoegenAanbieding(Aanbieding aanbieding)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    authDBController.InsertAanbieding(aanbieding);
                    return RedirectToAction("OverzichtAanbiedingen", "Aanbieding");
                }
                else
                {
                    return View();
                }


            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "er is iets fout gegaan:" + e;
                return View();
            }

        }

        [HttpPost]
        public ActionResult CreateAanbieding(String soort, int percentage, String actief)
        {
            bool isActief = actief == "on";
            Aanbieding aanbieding = new Aanbieding { soort = soort, percentage = percentage, actief = isActief };
            try
            {
                authDBController.InsertAanbieding(aanbieding);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;
            }
            return RedirectToAction("OverzichtAanbiedingen", "Aanbieding");
        }

        [HttpPost]
        public ActionResult CreateAanbiedingModelBinding(Aanbieding aanbieding)
        {
            try
            {
                authDBController.InsertAanbieding(aanbieding);

            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;
            }
            return RedirectToAction("OverzichtAanbiedingen", "Aanbieding");
        }



        public ActionResult VerwijderAanbieding(int aanbiedingId)
        {
            try
            {
                authDBController.DeleteAanbieding(aanbiedingId);
            }

            catch (Exception e)
            {
                ViewBag.Foutmelding = ("Er is iets fout gegeaan" + e);
            }
            return RedirectToAction("OverzichtAanbiedingen", "Aanbieding");
        }


        [HttpPost]
        public ActionResult WijzigenAanbieding(Aanbieding aanbieding)
        {
            Console.WriteLine(aanbieding);
            try
            {
                authDBController.UpdateAanbieding(aanbieding);

            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;

            }
            return RedirectToAction("OverzichtAanbiedingen", "Aanbieding");
        }



    }
}
