using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class ProductType
    {
        public int ID_PT { get; set; }
        public String Naam { get; set; }
        public float InkoopPrijs { get; set; }
        public float VerkoopPrijs { get; set; }
        public String Omschrijving { get; set; }

        public String ImagePath { get; set; }

        public int Zichtbaar { get; set; }
        public double Aanbieding { get; set; }



        public override string ToString()
        {

            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", Naam, InkoopPrijs, VerkoopPrijs, Omschrijving, ImagePath, Zichtbaar, Aanbieding);

        }

    }
}
