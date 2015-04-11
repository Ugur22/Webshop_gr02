using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Gebruiker
    {
        public int ID_G { get; set; }
        public string Voornaam { get; set; }
        public string Tussenvoegsel { get; set; }
        public string Achternaam { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Geslacht { get; set; }
        public int ID_rol { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", Voornaam, Tussenvoegsel, Achternaam, Username, Password, Email, Geslacht, ID_rol);
        }
    }
}