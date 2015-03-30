﻿using System;
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

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", naam, voorraad, zichtbaar);
        }
    }
}