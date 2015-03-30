using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WorkshopASPNETMVC3_IV_.Controllers
{
    public class SessionController : Controller
    {
        //
        // GET: /Session/

        public ActionResult Index()
        {

            //Checken of sessie object al bestaat.
            Object laatsteKeerBezocht = Session["DateTimeLaatsteKeerBezocht"];

            if (laatsteKeerBezocht != null)
            {
                //DateTime op de viewbag plaatsen
                ViewBag.DatumLaatsteKeer = (DateTime)Session["DateTimeLaatsteKeerBezocht"];
            }
            else
            {
                ViewBag.DatumLaatsteKeer = "Je bent voor het eerst op deze pagina!";
            }
           
            //Sessie variabelen opnieuw zetten met huidige DateTime
            Session["DateTimeLaatsteKeerBezocht"] = DateTime.Now;
            //Retourneren View
            return View();
            }
        }

    }

