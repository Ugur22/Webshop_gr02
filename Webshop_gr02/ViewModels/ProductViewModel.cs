using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;

namespace Webshop_gr02.ViewModels
{
    public class ProductViewModel
    {


        public ProductType ProductType { get; set; }
        public SelectList Products { get; set; }

        public int SelectedProductID { get; set; }
    }
}