using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webshop_gr02.Models;
using System.Data.Entity;

namespace Webshop_gr02.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> producten { get; set; }
    }
}