using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.ViewComponents.Shared
{
    public class CartVm
    {
        public int BadgeCount { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItem> Products { get; set; }
    }
}
