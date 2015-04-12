using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webshop_gr02.Models
{
    public class Categorie
    {
        public int ID_C { get; set; }
        [Required(ErrorMessage = "Naam is een verplicht veld")]
        [StringLength(20, ErrorMessage = "De naam mag maximaal 20 karakers bevatten.")]
        [Remote("IsEmailAvailable", "CategorieController")]
        public String Naam { get; set; }

        public override string ToString()
        {
            return String.Format("{0}(ID_C {1}) {2}", ID_C, Naam);
        }
    }
}