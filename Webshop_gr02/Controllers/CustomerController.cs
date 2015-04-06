﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;
using Webshop_gr02.ViewModels;

namespace Webshop_gr02.Controllers
{
    public class CustomerController : Controller
    {
        
        //
        // GET: /Customer/

        private AuthDBController authDBController = new AuthDBController();

        public ActionResult ProductenOverzichtklant()
        {
            try
            {
            
                List<ProductType> product = authDBController.GetTypeLijst();
                return View(product);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

        public ActionResult ProductTypeBestellen(string productTypeId)
        {
            try
            {
                //Viewmodel aanmaken
                ProductTypeAanbiedingen viewModel = new ProductTypeAanbiedingen();
                //Te wijzigen game ophalen
                ProductType productType = authDBController.GetProductType(productTypeId);

                //Viewmodel vullen
                viewModel.ProductType = productType;


                //View retourneren met viewModel
                return View(viewModel);
            }
            catch (Exception e)
            {
                ViewBag.FoutMelding = "Er is iets fout gegaan: " + e;
                return View();
            }



        }

    }
}
