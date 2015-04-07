using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_gr02.Models;

namespace Webshop_gr02.Controllers
{
    public class Item
    {

        private Product product = new Product();

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

     
        private int quantity;

        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public Item()
        {
        }
        public Item(Product product, int quantity)
        {
            this.Product = product;
            this.quantity = quantity;
        }

    }
}