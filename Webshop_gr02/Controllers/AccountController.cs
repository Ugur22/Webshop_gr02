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

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public ActionResult ToevoegenRegistratie(Registratie registratie)
        {
            int ID_rol = 0;
            string email = "";
            
            try
            {
                if (ModelState.IsValid)
                {
                    bool auth = authDBController.checkUsername(registratie.Username);

                    if (auth==true)
                    {
                        bool emailauth = authDBController.checkEmail(registratie.Email);

                        if (emailauth == true)
                        {
                            email = registratie.Email;
                            ID_rol = authDBController.HaalRolID();
                            authDBController.InsertRegistratie(registratie, ID_rol);
                            authDBController.InsertKlant(registratie, email);
                        }
                        else {
                            ModelState.AddModelError("emailfout", "Dit email-adres wordt al gebruikt.");
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("registratiefout", "Deze gebruikersnaam wordt al gebruikt");
                        return View();
                    }
                }
                else {
                    return View();
                }
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

        [Authorize(Roles = "BEHEERDER")]
        public ActionResult ToevoegenAdmin()
        {
            try
            {
                RegisterAdminViewModel viewModel = new RegisterAdminViewModel();
                List<GebruikersRollen> gebruikersRollen = authDBController.getAllGebruikersRollen();
                viewModel.Rollen = new SelectList(gebruikersRollen, "rol_id", "rolnaam");
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
        public ActionResult ToevoegenAdmin(RegisterAdminViewModel viewModel)
        {
            Console.WriteLine(viewModel);
            try
            {
                if (ModelState.IsValid)
                {

                    bool auth = authDBController.checkUsername(viewModel.Registratie.Username);

                    if (auth)
                    {
                        viewModel.Registratie.rol_id = viewModel.SelectedRolID;
                        
                        authDBController.InsertAdmin(viewModel.Registratie);
                        return RedirectToAction("OverzichtGebruiker", "Gebruiker");
                    }
                    else
                    {
                        ModelState.AddModelError("productfout", "Product bestaat al voer een andere naam in");
                        return View();
                    }
                }
                else
                {
                    //viewModel.Aanbiedingen = GetAanbiedingen();
                    List<GebruikersRollen> gebruikersRollen = authDBController.getAllGebruikersRollen();
                    viewModel.Rollen = new SelectList(gebruikersRollen, "rol_id", "rolnaam");
                    return View(viewModel);
                }
            }
            catch (Exception e)
            {
                ViewBag.Foutmelding = "Er is iets fout gegeaan" + e;
                return View();
            }
        }
    }
}
