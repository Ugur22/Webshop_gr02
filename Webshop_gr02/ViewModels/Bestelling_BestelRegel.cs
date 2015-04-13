using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_gr02.Models;

namespace Webshop_gr02.ViewModels
{
    public class Bestelling_BestelRegel
    {
            public Bestelling bestelling { get; set; }
            public Product product { get; set; }
            public BestelRegel bestelRegel { get; set; }
            
    }
}