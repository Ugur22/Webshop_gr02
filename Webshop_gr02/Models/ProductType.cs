using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class ProductType
    {
        [Key]
        [Column(Order = 0)]

        public int ID_PT { get; set; }
        [Required(ErrorMessage = "Naam is een verplicht veld")]
        [RegularExpression("([a-zA-Z z0-9_-]{2,20}\\s*)+", ErrorMessage = "Geen geldige naam voor een product type")]
        public String Naam { get; set; }
        [Required(ErrorMessage = "InkoopPrijs is een verplicht veld")]
        public float InkoopPrijs { get; set; }
        [Required(ErrorMessage = "VerkoopPrijs is een verplicht veld")]
        public float VerkoopPrijs { get; set; }
        [Required(ErrorMessage = "Omschrijving is een verplicht veld")]
        [DataType(DataType.MultilineText)]
        [StringLength(255)]
        public String Omschrijving { get; set; }
        [Required(ErrorMessage = "Merk is een verplicht veld")]
        [RegularExpression("([a-zA-Z]{2,20}\\s*)+", ErrorMessage = "Geen geldige Merk voor een producttype")]
        public String Merk { get; set; }
        [Required(ErrorMessage = "ImagePath is een verplicht veld")]
        [DataType(DataType.ImageUrl)]
        public String ImagePath { get; set; }
        public Boolean Zichtbaar { get; set; }
        public int ID_A;
        public Aanbieding Aanbieding { get; set; }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}  ", ID_PT, Naam, InkoopPrijs, VerkoopPrijs, Omschrijving, ImagePath, Zichtbaar, Aanbieding, Merk);
        }

    }
}
