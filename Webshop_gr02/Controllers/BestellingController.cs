﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;

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
                List<BestelRegel> bestelRegels = authDBController.GetBestellingOverzicht();
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
                return View(bestelRegels);
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

    }
}