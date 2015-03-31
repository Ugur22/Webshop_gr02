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

<<<<<<< HEAD
        public ActionResult ToevoegenProductType()
        {
            return View();
        }


        //public ActionResult Index()
        //{
        //    return View();
        //}



      
=======
>>>>>>> origin/master
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

<<<<<<< HEAD
               }
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
                //authDBController.InsertProduct(product);
                return View();
            }
>>>>>>> origin/master
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
<<<<<<< HEAD

        }


        public ActionResult ToevoegenProduct()
        {
            return View();
        }

   //     [HttpPost]
   //     public ActionResult ToevoegenProduct(Product product)
   //     {
   //         try
   //         {
   //             authDBController.InsertProduct(product);


   //         }
   //         catch (Exception e)
   //         {
   //ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
   //             return View();  
   //         }


   //     }

   //             ViewBag.Foutmelding = "er is iets fout gegaan:" + e;


   //         }
   //         return RedirectToAction("LogOn", "Account");
   //     }


=======
        }
>>>>>>> origin/master
    }
}
