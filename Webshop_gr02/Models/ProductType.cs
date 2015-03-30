﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class ProductType
    {

        public String Naam { get; set; }
        public double InkoopPrijs { get; set; }
        public double VerkoopPrijs { get; set; }
        public String Omschrijving { get; set; }
        public String ImageName { get; set; }
        public String Zichtbaar { get; set; }
        public String Aanbieding { get; set; }



        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", Naam, InkoopPrijs, VerkoopPrijs, Omschrijving, ImageName, Zichtbaar, Aanbieding);
        }

    }
}