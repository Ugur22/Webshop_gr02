﻿using System;
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
        public String Zichtbaar { get; set; }
        public String Aanbieding { get; set; }



        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}",ID_PT, Naam, InkoopPrijs, VerkoopPrijs, Omschrijving, ImagePath, Zichtbaar, Aanbieding);
        }

    }
}
