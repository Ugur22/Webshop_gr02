﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;
using WorkshopASPNETMVC3_IV_.Models;
using System.IO;

namespace Webshop_gr02.Controllers
{
    public class GoldMemberController : Controller
    {
        private AuthDBController authDBController = new AuthDBController();

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult GoldMemberOverzicht()
        {
            try
            {
                List<GoldMember> goldMemberLijst = authDBController.getGoldMember();
                return View(goldMemberLijst);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult WijzigGoldMember(int ID_GM)
        {
            try
            {
                GoldMember goldMember = authDBController.GetGM(ID_GM);
                return View(goldMember);
            }
            catch (Exception e)
            {
                ViewBag.FoutMelding = "Er is iets fout gegaan: " + e;
                return View();
            }
        }

        [Authorize(Roles = "BEHEERDER")]
        [HttpPost]
        public ActionResult WijzigGoldMember(GoldMember goldMember)
        {
            Console.WriteLine(goldMember);
            try
            {
                if (ModelState.IsValid)
                {
                    authDBController.UpdateGM(goldMember);
                    return RedirectToAction("GoldMemberOverzicht", "GoldMember");
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
    }
}
