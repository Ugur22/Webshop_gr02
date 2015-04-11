using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class GoldMember
    {


        public int ID_GM { get; set; }
        public float percentage { get; set; }
        public float min_bedrag { get; set; }
       

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", ID_GM, percentage, min_bedrag);
        }
    }
}