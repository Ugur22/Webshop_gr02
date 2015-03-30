using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop_gr02.Models
{
    public class ProductType
    {
        public int ID_PT { get; set; }
        public String Naam { get; set; }
        public float InkoopPrijs { get; set; }
        public float VerkoopPrijs { get; set; }
        public String Omschrijving { get; set; }
<<<<<<< HEAD
        public String ImagePath { get; set; }
=======
        public String image_path { get; set; }
>>>>>>> 32ea57dd55f2293eea07c970c860fddffed1467b
        public String Zichtbaar { get; set; }
        public String Aanbieding { get; set; }



        public override string ToString()
        {
<<<<<<< HEAD
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}",ID_PT, Naam, InkoopPrijs, VerkoopPrijs, Omschrijving, ImagePath, Zichtbaar, Aanbieding);
=======
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}", Naam, InkoopPrijs, VerkoopPrijs, Omschrijving, image_path, Zichtbaar, Aanbieding);
>>>>>>> 32ea57dd55f2293eea07c970c860fddffed1467b
        }

    }
}
