using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Bestelling
    {

        public int ID_B { get; set; }
        public BestelRegel bestelRegel { get; set; }
        public string status { get; set; }
        public string datum { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", ID_B, bestelRegel, status, datum);
        }


    }
}