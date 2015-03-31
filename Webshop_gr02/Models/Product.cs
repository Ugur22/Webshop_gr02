using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Product
    {
        public string naam { get; set; }
        public int voorraad { get; set; }
        public int zichtbaar { get; set; }
        public double prijs { get; set; }
        public int afzet { get; set; }
        public double BrutoOmzet { get; set; }
        public double NettoOmzet { get; set; }
        public ProductType productType { get; set;}
        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} ", naam, voorraad, zichtbaar, prijs, afzet, BrutoOmzet, NettoOmzet, productType );
        }
    }
}