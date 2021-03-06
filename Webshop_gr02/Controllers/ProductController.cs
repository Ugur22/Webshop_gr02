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
using System.IO;

namespace Webshop_gr02.Controllers
{
    public class ProductController : Controller
    {

        private AuthDBController authDBController = new AuthDBController();

        [Authorize(Roles = "BEHEERDER")]
        [HttpPost]
        public ActionResult ToevoegenProductType(ProductTypeAanbiedingen viewModel, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool auth = authDBController.checkproducttype(viewModel.ProductType.Naam);

                    if (!auth)
                    {

                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                            file.SaveAs(path);
                        }


                        viewModel.ProductType.Aanbieding = authDBController.GetAanbieding(viewModel.SelectedAanbiedingID);
                        authDBController.InsertProductType(viewModel.ProductType);
                        return RedirectToAction("ProductTypeOverzicht", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("producttypefout", "producttype bestaat al voer een andere naam in");
                        viewModel.Aanbiedingen = GetAanbiedingen();
                        return View(viewModel);
                    }
                }
                else
                {
                    viewModel.Aanbiedingen = GetAanbiedingen();
                    return View(viewModel);
                }
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }

        }

        [Authorize(Roles = "BEHEERDER")]
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

        [Authorize(Roles = "BEHEERDER")]
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

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult ToevoegenProduct()
        {
            try
            {
                ProductTypeViewModel viewModel = new ProductTypeViewModel();
                List<ProductType> productType = authDBController.GetAllProductTypes();
                viewModel.Eigenschapwaarde = GetEigenschapwaarde();
                viewModel.ProductType = new SelectList(productType, "ID_PT", "Naam");
                return View(viewModel);
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

        [Authorize(Roles = "BEHEERDER")]
        [HttpPost]
        public ActionResult ToevoegenProduct(ProductTypeViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {



                    bool auth = authDBController.checkProducttoevoegn(viewModel.Product.naam);

                    if (auth)
                    {
                        viewModel.Product.productType = authDBController.GetProductType(viewModel.SelectedProductTypeID);
                        viewModel.Product.eigenschapwaarde = authDBController.GetEigenschapWaarde(viewModel.SelectedeigenschapwaardeID);
                        authDBController.InsertProduct(viewModel.Product);
                        return RedirectToAction("ProductenOverzicht", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("productfout", "Product bestaat al voer een andere naam in");
                        viewModel.Eigenschapwaarde = GetEigenschapwaarde();
                        viewModel.ProductType = GetProducttype();
                        return View(viewModel);
                    }
                }
                else
                {
                    viewModel.Eigenschapwaarde = GetEigenschapwaarde();
                    viewModel.ProductType = GetProducttype();
                    return View(viewModel);
                }
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }

        [Authorize(Roles = "BEHEERDER")]
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

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult verwijderenProduct(int ProductId)
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

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult ProductWijzigen(string productId)
        {
            try
            {
                ProductTypeViewModel viewModel = new ProductTypeViewModel();
                List<ProductType> productType = authDBController.GetAllProductTypes();
                Product Product = authDBController.GetProduct(productId);
                viewModel.Eigenschapwaarde = GetEigenschapwaarde();
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

        [Authorize(Roles = "BEHEERDER")]
        [HttpPost]
        public ActionResult ProductWijzigen(ProductTypeViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {



                    viewModel.Product.productType = authDBController.GetProductType(viewModel.SelectedProductTypeID);
                    viewModel.Product.eigenschapwaarde = authDBController.GetEigenschapWaarde(viewModel.SelectedeigenschapwaardeID);
                    authDBController.UpdateProduct(viewModel.Product);
                    return RedirectToAction("ProductenOverzicht", "Product");

                }
                else
                {
                    viewModel.Eigenschapwaarde = GetEigenschapwaarde();
                    viewModel.ProductType = GetProducttype();
                    return View(viewModel);
                }
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegaan:" + e;
                return View();
            }
        }

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult ToevoegenProductType()
        {
            try
            {
                ProductTypeAanbiedingen viewModel = new ProductTypeAanbiedingen();
                viewModel.Aanbiedingen = GetAanbiedingen();
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

        private SelectList GetEigenschapwaarde()
        {
            List<Eigenschapwaarde> eigenschapwaarde = authDBController.GetEigenschapwaardes();
            Eigenschapwaarde emptyeigenscapwaarde = new Eigenschapwaarde { ID_EW = 0, waarde = "" };
            eigenschapwaarde.Insert(0, emptyeigenscapwaarde);
            return new SelectList(eigenschapwaarde, "ID_EW", "waarde");
        }


        private SelectList GetProduct()
        {
            List<Product> product = authDBController.GetProductLijst();
            Product emptyproduct = new Product { ID_P = 0, naam = "" };
            product.Insert(0, emptyproduct);
            return new SelectList(product, "ID_P", "naam");
        }



        private SelectList GetProducttype()
        {
            List<ProductType> ProductType = authDBController.GetTypeLijst();
            ProductType emptyProductType = new ProductType { ID_PT = 0, Naam = "" };
            ProductType.Insert(0, emptyProductType);
            return new SelectList(ProductType, "ID_PT", "naam");
        }

        [Authorize(Roles = "BEHEERDER")]
        [HttpPost]
        public ActionResult WijzigProductType(ProductTypeAanbiedingen viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    viewModel.ProductType.Aanbieding = authDBController.GetAanbieding(viewModel.SelectedAanbiedingID);
                    authDBController.UpdateProductType(viewModel.ProductType);
                    return RedirectToAction("ProductTypeOverzicht", "Product");

                }
                else
                {
                    viewModel.Aanbiedingen = GetAanbiedingen();
                    return View(viewModel);
                }
            }
            catch (Exception e)
            {
                ViewBag.FoutMelding("Er is iets fout gegaan: " + e);
                return View();
            }
        }

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult WijzigProductType(int productTypeId)
        {
            try
            {
                //Viewmodel aanmaken
                ProductTypeAanbiedingen viewModel = new ProductTypeAanbiedingen();
                //Te wijzigen game ophalen
                ProductType productType = authDBController.GetProductType(productTypeId);

                //Viewmodel vullen
                viewModel.ProductType = productType;
                viewModel.SelectedAanbiedingID = productType.Aanbieding == null ? 0 : productType.Aanbieding.ID_A;
                //SelectList ophalen voor aanbieding.
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
