using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_gr02.Models;

namespace Webshop_gr02.Models
{
    public class Eigenschapwaarde
    {

        public int ID_EW { get; set; }
        public string waarde { get; set; }

            public override string ToString()
        {
            return String.Format("{0}(ID_EW {1}) {2} {3}", ID_EW, waarde);
        }
    }
}