using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Webshop_gr02.Models
{
    public class Registratie
    {
        [Required(ErrorMessage= "Vul uw voornaam in")]
        [RegularExpression("([a-zA-Z]{2,20}\\s*)+", ErrorMessage = "Geen geldige voornaam")]
        public String Voornaam { get; set; }

        
        [RegularExpression("([a-zA-Z]{2,10}\\s*)+", ErrorMessage = "Geen geldig tussenvoegsel")]
        public String Tussenvoegsel { get; set; }


        [Required(ErrorMessage = "Vul uw achternaam in")]
        [RegularExpression("([a-zA-Z]{2,20}\\s*)+", ErrorMessage = "Geen geldige achternaam")]
        public String Achternaam { get; set; }

        [Required(ErrorMessage = "Vul een gebruikersnaam in")]
        [RegularExpression("^[a-z0-9_-]{3,25}$", ErrorMessage = "Geen geldige gebruikersnaam.")]
        public String Username { get; set; }

        [Required(ErrorMessage = "Vul een wachtwoord in")]
        [RegularExpression("^[A-Za-z0-9_]{6,25}$", ErrorMessage = "Geen geldig wachtwoord. Het wachtwoord moet ten minste 6 tekens lang zijn en mag geen @ of spatie bevatten.")]
        public String Password { get; set; }

        [Required(ErrorMessage = "Bevestig uw wachtwoord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Beide wachtwoorden komen niet overeen.")]
        public String Password2 { get; set; }

        [Required(ErrorMessage = "Vul uw email-adres in")]
        [RegularExpression("^[_A-Za-z0-9-]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Geen geldig email-adres.")]
        public String Email { get; set; }
        
        public String Geslacht { get; set; }
        
        public Klant klant { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", Voornaam, Tussenvoegsel, Achternaam, Username, Password, Email, Geslacht);
        }

    }


}