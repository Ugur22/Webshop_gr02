using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkshopASPNETMVC3_IV_.Controllers
{
    public class CookieController : Controller
    {
        //
        // GET: /Cookie/

        public ActionResult Index()
        {
           if (Request.Cookies["naam"] != null)
            {
               HttpCookie cookie = Request.Cookies["naam"];
               ViewBag.Naam = cookie.Value;
            }
  
            return View();
        }

        public ActionResult PlaatsCookieTest()
        {
            HttpCookie cookie = new HttpCookie("test");
            cookie.Value = "PlaatsCookieTest";
            cookie.Expires = DateTime.Now.AddMinutes(1);
            Response.Cookies.Add(cookie);
            
            return View();
        }

        public ActionResult HaalCookieOpTest()
        {
            HttpCookie cookie = Request.Cookies["test"];
            if (cookie != null)
            {
                ViewBag.Test = cookie.Value;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(String naam)
        {
            
            HttpCookie cookie = Request.Cookies["naam"];
            if (cookie == null)
            {
                cookie = new HttpCookie("naam");
                Response.Cookies.Add(cookie);
            }
            cookie.Expires = DateTime.Now.AddMinutes(1);
            cookie.Value = naam;
            
            return RedirectToAction("Index");
        }

    }
}
