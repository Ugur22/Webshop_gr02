using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;
using WorkshopASPNETMVC3_IV_.Models;

namespace WorkshopASPNETMVC3_IV_.Controllers
{
    public class AccountController : Controller
    {

        private AuthDBController authDBController = new AuthDBController();

        public ViewResult LogOn()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            return View();
        }

       
        [HttpPost]
        public ActionResult ToevoegenRegistratie(Registratie registratie)
        {
            try
            {
                authDBController.InsertRegistratie(registratie);

            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "er is iets fout gegaan:" + e;


            }
            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult ToevoegenRegistratie()
        {
            return View();
        }



        [HttpPost]
        public ActionResult LogOn(LogOnViewModel viewModel, String returnUrl)
        {
          
            if (ModelState.IsValid)
            {
                bool auth = authDBController.isAuthorized(viewModel.UserName, viewModel.PassWord);

                if (auth)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.UserName, false);
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                  
                }
                else
                {
                    ModelState.AddModelError("loginfout", "Username en Password zijn incorrect");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

    }
}
