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
        public String Naam { get; set; }
        public float InkoopPrijs { get; set; }
        public float VerkoopPrijs { get; set; }
        public String Omschrijving { get; set; }
        public String Merk { get; set; }
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
