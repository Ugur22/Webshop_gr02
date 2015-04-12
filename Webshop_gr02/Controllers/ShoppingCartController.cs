﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;

namespace Webshop_gr02.Controllers
{
    public class ShoppingCartController : Controller
    {

        ProductContext storeDB = new ProductContext();

        List<Item> cart = new List<Item>();

        public ActionResult Index()
        {

            return View();
        }

        private int isExisting(int id)
        {
            cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ID_P == id)
                    return i;
            return -1;
        }

        public ActionResult Delete(int id)
        {
            int index = isExisting(id);
            cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("Cart");
        }

        public ActionResult OrderNow(int id)
        {
            if (Session["cart"] == null)
            {


                cart.Add(new Item(storeDB.producten.Find(id), 1));
                Session["cart"] = cart;

            }
            else
            {
                cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                    cart.Add(new Item(storeDB.producten.Find(id), 1));
                else
                    cart[index].Quantity++;
                Session["cart"] = cart;
            }
            return View("Cart");
        }

    }
}