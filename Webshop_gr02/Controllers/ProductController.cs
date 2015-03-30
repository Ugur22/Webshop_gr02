using System;
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

        public ActionResult ToevoegenProductType()
        {
            return View();
        }

<<<<<<< HEAD
        //public ActionResult Index()
        //{
        //    return View();
        //}



      
        public ActionResult ProductTypeOverzicht()
        {
            try
            {
                List<ProductType> producten = authDBController.GetProductTypeOverzicht();
                return View(producten);

=======
        public ActionResult ToevoegenProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ToevoegenProduct(Product product)
        {
            try
            {
                authDBController.InsertProduct(product);
>>>>>>> 32ea57dd55f2293eea07c970c860fddffed1467b

            }
            catch (Exception e)
            {
<<<<<<< HEAD
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }


        }
=======
                ViewBag.Foutmelding = "er is iets fout gegaan:" + e;


            }
            return RedirectToAction("LogOn", "Account");
        }
        
>>>>>>> 32ea57dd55f2293eea07c970c860fddffed1467b

    }
}
