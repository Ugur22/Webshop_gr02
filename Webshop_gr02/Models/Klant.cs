using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Klant
    {

        public int ID_G { get; set; }
        public string postcode { get; set; }
        public string huisnummer { get; set; }
        public int ID_GM { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", ID_G, postcode, huisnummer, ID_GM);
        }
    }
}