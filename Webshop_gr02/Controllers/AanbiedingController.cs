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

        [Authorize(Roles = "BEHEERDER")]
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

        [Authorize(Roles = "BEHEERDER")]
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

        [Authorize(Roles = "BEHEERDER")]
        [HttpPost]
        public ActionResult WijzigAanbieding(Aanbieding aanbieding)
        {
            Console.WriteLine(aanbieding);
            try
            {
                if (ModelState.IsValid)
                {
                    bool auth = authDBController.checkAanbieding(aanbieding.soort);

                    if (!auth)
                    {
                        authDBController.UpdateAanbieding(aanbieding);
                        return RedirectToAction("OverzichtAanbiedingen", "Aanbieding");
                    }
                    else
                    {
                        ModelState.AddModelError("aanbiedingfout", "Aanbieding bestaat al voer een andere soort in");
                        return View();
                    }
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

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult ToevoegenAanbieding()
        {
            return View();
        }

        [Authorize(Roles = "BEHEERDER")]
        [HttpPost]
        public ActionResult ToevoegenAanbieding(Aanbieding aanbieding)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool auth = authDBController.checkAanbieding(aanbieding.soort);

                    if (!auth)
                    {
                        authDBController.InsertAanbieding(aanbieding);
                        return RedirectToAction("OverzichtAanbiedingen", "Aanbieding");
                    }
                    else
                    {
                        ModelState.AddModelError("aanbiedingfout", "Aanbieding bestaat al voer een andere soort in");
                        return View();
                    }
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

        [Authorize(Roles = "BEHEERDER")]
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

        [Authorize(Roles = "BEHEERDER")]
        [HttpPost]
        public ActionResult WijzigenAanbieding(Aanbieding aanbieding)
        {
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
