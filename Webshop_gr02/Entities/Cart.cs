using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_gr02.Models;
using System.Linq;

namespace Webshop_gr02.Entities
{
    public class Cart
    {


        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.Where(p => product.ID_P == product.ID_P).FirstOrDefault();

            if (line == null)
            {

                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {

                line.Quantity += quantity;
            }

        }
        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ID_P == product.ID_P);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }



}