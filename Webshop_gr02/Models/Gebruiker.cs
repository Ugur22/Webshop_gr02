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

        [Required(ErrorMessage = "Voornaam moet ingevuld zijn")]
        public string Voornaam { get; set; }

        public string Tussenvoegsel { get; set; }

        [Required(ErrorMessage = "Achternaam moet ingevuld zijn")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Username moet ingevuld zijn")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password moet ingevuld zijn")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email adres moet ingevuld zijn")]
        [DataType(DataType.EmailAddress)]
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