using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Aanbieding
    {
        public int  ID_A { get; set; }
        [Required(ErrorMessage = "Soort is een verplicht veld")]
        public string soort { get; set; }

           [Required(ErrorMessage = "percentage is een verplicht veld")]
        public int percentage { get; set; }

        public bool actief { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", ID_A, soort, percentage, actief);
        }
    }
}