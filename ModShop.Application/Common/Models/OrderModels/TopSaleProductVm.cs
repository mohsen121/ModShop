using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Common.Models.OrderModels
{
    public class TopSaleProductVm
    {
        public List<TopSaleProductItem> Products { get; set; }
    }

    public class TopSaleProductItem
    {
        public string Title { get; set; }
        public int SaleCount { get; set; }
    }
}
