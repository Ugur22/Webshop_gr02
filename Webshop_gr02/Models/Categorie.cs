using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Categorie
    {
        public int ID_C { get; set; }
        public String Naam { get; set; }

        public override string ToString()
        {
            return String.Format("{0}(ID_C {1}) {2}", ID_C, Naam);
        }
    }
}