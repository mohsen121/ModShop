using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages.Home
{
    public class ProductPageVm
    {
        public Product Parent { get; set; }
        public Product Selected { get; set; }
        public List<Product> Children { get; set; }
        public List<Size> ExistingSizes { get; set; }
        public List<Color> ExistingColors { get; set; }
        public bool IsInStock { get; set; }
    }
}
