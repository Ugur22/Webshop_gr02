using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_gr02.Models;

namespace Webshop_gr02.Controllers
{
    public class Item
    {

        private ProductType producttype = new ProductType();

        public ProductType Producttype
        {
            get { return producttype; }
            set { producttype = value; }
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
        public Item(ProductType producttype, int quantity)
        {
            this.producttype = producttype;
            this.quantity = quantity;
        }

    }
}