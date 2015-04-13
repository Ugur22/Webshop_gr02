﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.DatabaseControllers;
using Webshop_gr02.Models;

namespace WorkshopASPNETMVC3_IV_.Controllers
{

    public class HomeController : Controller
    {


        private AuthDBController authDBController = new AuthDBController();
        //
        // GET: /Home/



        public ActionResult Index()
        {
            string username = User.Identity.Name;
            bool goldmember = false;
            string welOfNiet = "";
            float percentage = 0;
            float nieuweprijs = 1;
            try
            {

                List<Product> product = authDBController.GetProductLijst();
                if (User.Identity.IsAuthenticated)
                {
                    goldmember = authDBController.ControleerGoldMember(username);
                }
                else
                {
                    goldmember = false;
                }
                percentage = authDBController.haalPercentageGM();

                if (percentage > 0)
                {
                    nieuweprijs = (100 - percentage) / 100;
                }


                if (goldmember == true)
                {
                    welOfNiet = "Gefeliciteerd! Je bent GoldMember.";

                }
                else
                {
                    welOfNiet = "Je bent geen GoldMember";
                }
                ViewBag.percentage = percentage;

                ViewBag.tekstgm = welOfNiet;
                ViewBag.goldmember = goldmember;
                ViewBag.nieuweprijs = nieuweprijs;

                return View(product);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }
        
        [Authorize(Roles = "BEHEERDER")]
        public ActionResult Gold()
        {
            return View();
        }

        [Authorize(Roles = "MANAGER")]
        public ActionResult Silver()
        {
            return View();
        }
    }
}
