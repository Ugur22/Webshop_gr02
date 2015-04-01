﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;
using WorkshopASPNETMVC3_IV_.Models;

namespace Webshop_gr02.Controllers
{
    public class ProductController : Controller
    {

        private AuthDBController authDBController = new AuthDBController();

        public ActionResult ToevoegenProductType()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ToevoegenProductType(ProductType productType)
        {
            try
            {
                authDBController.InsertProductType(productType);

            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "er is iets fout gegaan:" + e;


            }
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult ProductTypeOverzicht()
        {
            try
            {
                List<ProductType> producten = authDBController.GetTypeLijst();
                return View(producten);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }
 
        public ActionResult ToevoegenProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ToevoegenProduct(Product product, ProductType productType)
        {
            try
            {
                authDBController.InsertProduct(product, productType);
                return View();
            }

            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }


        public ActionResult CreateProductType(int ID_PT, String naamProduct, float inkoop_prijs, float verkoopPrijs, String omschrijving, String imagePath, int zichtbaar, double aanbieding, String merk)
        {
           // bool isZichtbaar = zichtbaar == 1;
            ProductType productType = new ProductType { ID_PT = ID_PT, Naam = naamProduct, InkoopPrijs = inkoop_prijs, VerkoopPrijs = verkoopPrijs, Omschrijving = omschrijving, ImagePath = imagePath, Zichtbaar = zichtbaar, Aanbieding = aanbieding, Merk = merk };
            try
            {
                authDBController.InsertProductType(productType);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;
            }
            return RedirectToAction("Index", "Genre");
        }

        //public ActionResult CreateGenreParameterBinding(String name, String verslavend)
        //{
        //    bool isVerslavend = verslavend == "on";
        //    Genre genre = new Genre { Naam = name, Verslavend = isVerslavend };
        //    try
        //    {
        //        genreDBController.InsertGenre(genre);
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;
        //    }
        //    return RedirectToAction("Index", "Genre");
        //}




        //public ActionResult WijzigGenre(int genreId)
        //{
        //    try
        //    {
        //        Genre genre = genreDBController.GetGenre(genreId);
        //        return View(genre);
        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.FoutMelding("Er is iets fout gegaan: " + e);
        //        return View();
        //    }
        //}

        //public ActionResult WijzigGenre(Genre genre)
        //{
        //    try
        //    {
        //        genreDBController.UpdateGenre(genre);

        //    }
        //    catch (Exception e)
        //    {
        //        ViewBag.FoutMelding("Er is iets fout gegaan: " + e);

        //    }
        //    return RedirectToAction("Index", "Genre");


        //}


        public ActionResult WijzigProductType(ProductType productType)
        {
            try
            {
                authDBController.UpdateProductType(productType);
                return View(productType);
            }
            catch (Exception e)
            {
                ViewBag.FoutMelding("Er is iets fout gegaan: " + e);
                return View();
            }
        }
    }
}
