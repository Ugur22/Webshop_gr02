using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webshop_gr02.Models
{
    interface IProductRepository
    {
        IQueryable<Product> Products { get; } 
    }
}
