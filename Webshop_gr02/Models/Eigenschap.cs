using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Eigenschap
    {
        public int ID_E { get; set; }
        public string naam { get; set; }

        public override string ToString()
        {
            return String.Format("{0}(ID_E {1}) {2}", ID_E, naam);
        }
    }
}