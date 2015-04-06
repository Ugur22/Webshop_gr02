﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;
using WorkshopASPNETMVC3_IV_.Models;
using Webshop_gr02.ViewModels;

namespace Webshop_gr02.Controllers
{
    public class ProductController : Controller
    {

        private AuthDBController authDBController = new AuthDBController();


        [HttpPost]
        public ActionResult ToevoegenProductType(ProductTypeAanbiedingen viewModel)
        {
            try
            {
                viewModel.ProductType.Aanbieding = authDBController.GetAanbieding(viewModel.SelectedAanbiedingID);

                authDBController.InsertProductType(viewModel.ProductType);
                return RedirectToAction("ProductTypeOverzicht", "Product");

            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }

        }





        public ActionResult ProductTypeOverzicht()
        {
            try
            {
                List<ProductType> producten = authDBController.GetTypeLijst();
                Console.WriteLine(producten);
                return View(producten);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

        public ActionResult ProductenOverzicht()
        {
            try
            {
                List<Product> productenLijst = authDBController.GetProductLijst();
                return View(productenLijst);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }
 
        public ActionResult ToevoegenProduct()
        {
            try
            {
                ProductTypeViewModel viewModel = new ProductTypeViewModel();

                List<ProductType> productType = authDBController.GetAllProductTypes();

                viewModel.ProductType = new SelectList(productType, "ID_PT", "Naam");

                return View(viewModel);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

        [HttpPost]
        public ActionResult ToevoegenProduct(ProductTypeViewModel viewModel)
        {
            try
            {
                viewModel.Product.productType = authDBController.GetProductType(viewModel.SelectedProductTypeID.ToString());
                
                authDBController.InsertProduct(viewModel.Product);
                return RedirectToAction("ProductenOverzicht", "Product");
            }

            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

        public ActionResult verwijderenProductType(string ProductId)
        {
            try
            {
                authDBController.verwijderProductType(ProductId);
                return RedirectToAction("ProductTypeOverzicht", "Product");
            }

            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;

                return View();
            }
        }


        public ActionResult verwijderenProduct(string ProductId)
        {
            try
            {
                authDBController.verwijderProduct(ProductId);
                return RedirectToAction("ProductenOverzicht", "Product");
            }

            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

        public ActionResult ProductWijzigen(string productId)
        {
            try
            {
                ProductTypeViewModel viewModel = new ProductTypeViewModel();

                List<ProductType> productType = authDBController.GetAllProductTypes();

                Product Product = authDBController.GetProduct(productId);

                viewModel.ProductType = new SelectList(productType, "ID_PT", "Naam", Product.productType.ID_PT);

                viewModel.Product = Product;

                return View(viewModel);
            }
            catch (Exception e)
            {
                ViewBag.FoutMelding("Er is iets fout gegaan: " + e);
                return View();
            }
        }

        [HttpPost]
        public ActionResult ProductWijzigen(ProductTypeViewModel viewModel)
        {
            //Console.WriteLine(product);
            try
            {
               // viewModel.ProductType.Aanbieding = authDBController.GetAanbieding(viewModel.SelectedAanbiedingID);
               // authDBController.UpdateProductType(viewModel.ProductType);
                viewModel.Product.productType = authDBController.GetProductType(viewModel.SelectedProductTypeID);
                authDBController.UpdateProduct(viewModel.Product);
                return RedirectToAction("ProductenOverzicht", "Product");
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;
                return View();
            }


           



        }

        public ActionResult ToevoegenProductType()
        {

            try
            {
                ProductTypeAanbiedingen viewModel = new ProductTypeAanbiedingen();

                List<Aanbieding> aanbieding = authDBController.GetAanbiedingen();

                viewModel.Aanbiedingen = new SelectList(aanbieding, "ID_A", "soort");

                return View(viewModel);
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

        [HttpPost]
        public ActionResult WijzigProductType(ProductTypeAanbiedingen viewModel)
        {
            try
            {
                viewModel.ProductType.Aanbieding = authDBController.GetAanbieding(viewModel.SelectedAanbiedingID);
                authDBController.UpdateProductType(viewModel.ProductType);
                return RedirectToAction("ProductTypeOverzicht", "Product");

            }
            catch (Exception e)
            {
                ViewBag.FoutMelding("Er is iets fout gegaan: " + e);
                return View();
            }
        }

        public ActionResult WijzigProductType(string productTypeId)
        {
            try
            {
                //Viewmodel aanmaken
                ProductTypeAanbiedingen viewModel = new ProductTypeAanbiedingen();
                //Te wijzigen game ophalen
                ProductType productType = authDBController.GetProductType(productTypeId);

                //Viewmodel vullen
                viewModel.ProductType = productType;
                viewModel.SelectedAanbiedingID = productType.Aanbieding.ID_A;
                //SelectList ophalen voor genres.
                viewModel.Aanbiedingen = GetAanbiedingen();

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
