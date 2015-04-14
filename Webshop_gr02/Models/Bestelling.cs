using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class Bestelling
    {

        public int ID_B { get; set; }
        public int ID_K { get; set; }
        public List<BestelRegel> bestelRegel { get; set; }
        public string voornaam { get; set; }
        public string tussenvoegsel { get; set; }
        public string achternaam { get; set; }
        public double bedrag { get; set; }
        public string status { get; set; }
        public DateTime datum { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", ID_B, ID_K, bestelRegel, voornaam, tussenvoegsel, achternaam, bedrag, status, datum);
        }


    }
}