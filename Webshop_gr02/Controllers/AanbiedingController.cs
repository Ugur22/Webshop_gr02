﻿using System;
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

    }
}