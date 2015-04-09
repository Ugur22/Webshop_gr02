using System;
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
            bool goldmember = false;
            string welOfNiet = "";
            float percentage = 0;
            float nieuweprijs = 1;
            try
            {

                List<Product> product = authDBController.GetProductLijst();
                goldmember = authDBController.ControleerGoldMember();
                percentage = authDBController.haalPercentageGM();

                if (percentage > 0)
                {
                    nieuweprijs = (100 - percentage) / 100;
                }
                

                if (goldmember == true)
                {
                    welOfNiet = "Je bent GoldMember";

                }
                else
                {
                    welOfNiet = "Je bent geen GoldMember";
                }
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



    }
}
