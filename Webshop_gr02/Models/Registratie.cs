using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Registratie
    {
        
        public String Voornaam { get; set; }
        public String Tussenvoegsel { get; set; }
        public String Achternaam { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Password2 { get; set; }
        public String Email { get; set; }
        public String Geslacht { get; set; }
        public Klant klant { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", Voornaam, Tussenvoegsel, Achternaam, Username, Password, Email, Geslacht);
        }

    }


}