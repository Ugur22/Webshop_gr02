using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class GoldMember
    {


        public int ID_GM { get; set; }
        [Required(ErrorMessage = "percentage is een verplicht veld")]
        public float percentage { get; set; }
        [Required(ErrorMessage = "min_bedrag is een verplicht veld")]
        public float min_bedrag { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2}", ID_GM, percentage, min_bedrag);
        }
    }
}