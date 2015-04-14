using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;
using Webshop_gr02.DatabaseControllers;

namespace Webshop_gr02.Controllers
{
    public class ShoppingCartController : Controller
    {
        private AuthDBController authDBController = new AuthDBController();
        //ProductTypeEntities storeDB = new ProductTypeEntities();

        List<Product> productList = new List<Product>();
        Product product = new Product();

        public ActionResult Cart()
        {
            return View();
        }

        private int isExisting(int id)
        {
            List<Product> cart = (List<Product>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].ID_P == id)
                    return i;
            return -1;
        }

        public ActionResult Delete(int id)
        {
            int index = isExisting(id);
            List<Product> cart = (List<Product>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Cart");
        }

        public ActionResult OrderNow(int id)
        {
            string idString = id.ToString();
            Product product = authDBController.GetProduct_ProductType(idString);

            productList.Add(product);

            var sessionlist = Session["cart"] as List<Product>;
            if (Session != null && Session["cart"] != null)
            {
                for (int i = 0; i < sessionlist.Count(); i++)
                {
                    productList.Add(sessionlist[i]);
                }
            }
            Session["cart"] = productList;

            return RedirectToAction("Cart");
        }

        public ActionResult bestellen(string username)
        {
            int aantal = 1;
            var sessionlist = Session["cart"] as List<Product>;

            authDBController.InsertBestelling(username);

            int bestelnummer = authDBController.HaalBestelNummerUitDB(username);

            if (Session != null && Session["cart"] != null)
            {
                for (int i = 0; i < sessionlist.Count(); i++)
                {
                    authDBController.InsertBestelRegel(bestelnummer, sessionlist[i].ID_P, aantal, sessionlist[i].productType.VerkoopPrijs);
                    if (sessionlist[i].voorraad > 0)
                    {
                        int voorraad = sessionlist[i].voorraad - aantal;
                        authDBController.WijzigVoorraad(sessionlist[i].ID_P, voorraad);
                    }
                }
            }

            Session.Abandon();

            return RedirectToAction("BestellingGelukt", "Bestelling");
        }
    }
}