using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop_gr02.Models;

namespace Webshop_gr02.ViewModels
{
    public class ProductTypeViewModel
    {
        public Product Product { get; set; }
        public SelectList ProductType { get; set; }
        public SelectList Eigenschapwaarde { get; set; }

        public int SelectedProductTypeID { get; set; }
        public int SelectedeigenschapwaardeID { get; set; }
    }
}