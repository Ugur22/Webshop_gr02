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
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ID_P == id)
                    return i;
            return -1;
        }

        public ActionResult Delete(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Cart");
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

    }
}