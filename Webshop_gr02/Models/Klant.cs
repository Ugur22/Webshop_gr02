using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Webshop_gr02.Models
{
    public class Klant
    {

        public int ID_G { get; set; }

        [Required(ErrorMessage = "Vul uw postcode in")]
        [RegularExpression("^[1-9][0-9]{3}\\s?[a-zA-Z]{2}$", ErrorMessage = "Geen geldige postcode.")]
        public string postcode { get; set; }

        [Required(ErrorMessage = "Vul uw huisnummer in")]
        [RegularExpression("([0-9]{1,7})\\W*([a-zA-Z]{0,1})\\W*([0-9]{0,3})", ErrorMessage = "Geen geldig huisnummer.")]
        public string huisnummer { get; set; }

        public int ID_GM { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}", ID_G, postcode, huisnummer, ID_GM);
        }
    }
}