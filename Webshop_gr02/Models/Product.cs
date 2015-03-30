using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string naam { get; set; }
        public double prijs { get; set; }
        public int afzet { get; set; }
        public double BrutoOmzet { get; set; }
        public double NettoOmzet { get; set; }
        

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", ID, naam, prijs, afzet);
        }
    }
}