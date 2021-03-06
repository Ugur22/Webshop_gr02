﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Webshop_gr02.Models
{
    public class BestelRegel
    {
        public Bestelling bestelling { get; set; }
        public Product product { get; set; }
        public string naam { get; set; }
        public int ID_P { get; set; }
        public int ID_B { get; set; }
        public int aantal { get; set; }
        public double bedrag { get; set; }
        public DateTime datum { get; set; }
        public string status { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6}", bestelling, product, naam, aantal, bedrag, datum, status);
        }
    }
}