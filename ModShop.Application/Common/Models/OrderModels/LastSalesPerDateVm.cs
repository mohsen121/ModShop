using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Common.Models.OrderModels
{
    public class LastSalesPerDateVm
    {
        public List<LastSaleItem> Items { get; set; }
    }

    public class LastSaleItem
    {
        public DateTime Date { get; set; }
        public int SaleCount { get; set; }
    }
}
