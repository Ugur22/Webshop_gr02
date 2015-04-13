using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Gebruiker
    {
        public int ID_G { get; set; }

        [Required(ErrorMessage = "voornaam is een verplicht veld")]
        [RegularExpression("([a-zA-Z]{2,20}\\s*)+", ErrorMessage = "Geen geldige voornaam")]
        public string Voornaam { get; set; }

          [RegularExpression("([a-zA-Z]{2,10}\\s*)+", ErrorMessage = "Geen geldig tussenvoegsel")]
        public string Tussenvoegsel { get; set; }

        [Required(ErrorMessage = "Achternaam moet ingevuld zijn")]
        [RegularExpression("([a-zA-Z]{2,20}\\s*)+", ErrorMessage = "Geen geldige achternaam")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Username moet ingevuld zijn")]
        [RegularExpression("^[a-z0-9_-]{3,25}$", ErrorMessage = "Geen geldige gebruikersnaam.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password moet ingevuld zijn")]
        [RegularExpression("^[A-Za-z0-9_]{6,25}$", ErrorMessage = "Geen geldig wachtwoord. Het wachtwoord moet ten minste 6 tekens lang zijn en mag geen @ of spatie bevatten.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email adres moet ingevuld zijn")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[_A-Za-z0-9-]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9-]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Geen geldig email-adres.")]

        public string Email { get; set; }

        public string Geslacht { get; set; }
        public int ID_rol { get; set; }
        public Klant Klant { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", Voornaam, Tussenvoegsel, Achternaam, Username, Password, Email, Geslacht, ID_rol);
        }
    }
}