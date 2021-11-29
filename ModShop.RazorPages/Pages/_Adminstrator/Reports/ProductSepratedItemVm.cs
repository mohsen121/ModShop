using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Reports
{
    public class ProductSepratedItemVm
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
