using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Webshop_gr02.Controllers;
using Webshop_gr02.DatabaseControllers;
using Webshop_gr02.Models;

namespace Webshop_gr02.Controllers
{
    public class CategorieController : Controller
    {
        //
        // GET: /Categorie/


        private AuthDBController authDBController = new AuthDBController();





        [HttpPost]
        public ActionResult ToevoegenCategorie(Categorie categorie)
        {
            try
            {

                if (ModelState.IsValid)
                {


                    bool auth = authDBController.checkCategorie(categorie.Naam);

                    if (!auth)
                    {

                        authDBController.InsertCategorie(categorie);
                        return RedirectToAction("Overzichtcategorie", "Categorie");

                    }
                    else
                    {


                        ModelState.AddModelError("categoriefout", "Categorie bestaat al voer een andere naam in");
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


            }
            return View();
        }

        public ActionResult ToevoegenCategorie()
        {
            return View();
        }

        public ActionResult Overzichtcategorie()
        {
            try
            {
                List<Categorie> categorieën = authDBController.GetCategorieën();
                return View(categorieën);


            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }


        }


        public ActionResult VerwijderenCategorie(int ID_C)
        {
            try
            {

                authDBController.VerwijderCategorie(ID_C);

            }

            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;

                return View();
            }
            return RedirectToAction("Overzichtcategorie", "Categorie");
        }

        [HttpPost]
        public ActionResult WijzigenCategorie(Categorie categorie)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    bool auth = authDBController.checkCategorie(categorie.Naam);

                    if (!auth)
                    {
                        authDBController.UpdateCategorie(categorie);
                        return RedirectToAction("Overzichtcategorie", "Categorie");


                    }
                    else
                    {

                        ModelState.AddModelError("categoriefout", "Categorie bestaat al voer een andere naam in");
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
                ViewBag.FoutMelding("Er is iets fout gegaan: " + e);
                return View();
            }

        }

        public ActionResult WijzigenCategorie(int ID_C)
        {
            try
            {

                //Te wijzigen game ophalen
                Categorie categorie = authDBController.GetCategorie(ID_C);
                return View(categorie);
            }
            catch (Exception e)
            {
                ViewBag.FoutMelding = "Er is iets fout gegaan: " + e;
                return View();
            }



        }

    }
}
