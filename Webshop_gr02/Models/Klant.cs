using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Klant
    {
        public String Postcode { get; set; }
        public String Huisnummer { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1}", Postcode, Huisnummer);
        }
    }
}