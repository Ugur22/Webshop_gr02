using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Product
    {
        [Key]
        public int ID_P { get; set; }
        [Required(ErrorMessage = "Naam is een verplicht veld")]
        [RegularExpression("([a-zA-Z z0-9_-]{2,40}\\s*)+", ErrorMessage = "Geen geldige naam voor een product")]
        public string naam { get; set; }
        [Required(ErrorMessage = "voorraad is een verplicht veld")]
        public int voorraad { get; set; }
        public int zichtbaar { get; set; }
        public double prijs { get; set; }
        public int afzet { get; set; }
        public double BrutoOmzet { get; set; }
        public double NettoOmzet { get; set; }
        public int ID_EW { get; set; }
        public Eigenschapwaarde eigenschapwaarde { get; set; }
        public ProductType productType { get; set; }


        public interface IProductRepository
        {
            IEnumerable<Product> Getproducts();
            void UpdateProduct(Product product);
        }


        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", ID_P, naam, zichtbaar, prijs, afzet, BrutoOmzet, NettoOmzet, productType, eigenschapwaarde, ID_EW);
        }
    }
}