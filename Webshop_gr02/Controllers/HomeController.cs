using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkshopASPNETMVC3_IV_.Controllers
{
    
    public class HomeController : Controller
    {
        //
        // GET: /Home/
    
        public ActionResult Index()
        {
            if (User.IsInRole("BEHEERDER"))
            {
                return RedirectToAction("Gold");
            }
            else if (User.IsInRole("MANAGER"))
            {
                return RedirectToAction("Silver");
            }
            
            return View();
        }

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult Gold()
        {
            return View();
        }

        [Authorize(Roles = "MANAGER")]
        public ActionResult Silver()
        {
            return View();
        }

    }
}
