using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Webshop_gr02.Models;

namespace Webshop_gr02.Models
{
    public class ProductTypeEntities : DbContext
    {
        public DbSet<ProductType> producttypes { get; set; }
    }
}