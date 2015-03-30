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
using WorkshopASPNETMVC3_IV_.Models;

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
                authDBController.InsertCategorie(categorie);

            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "er is iets fout gegaan:" + e;


            }
            return RedirectToAction("Overzichtcategorie", "Categorie");
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
    }
}
