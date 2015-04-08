using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Webshop_gr02.Models;

namespace Webshop_gr02.ViewModels
{
    public class ProductTypeAanbiedingen
    {
        public ProductType ProductType { get; set; }

        public SelectList Aanbiedingen { get; set; }

        public int SelectedAanbiedingID { get; set; }
    }
}
