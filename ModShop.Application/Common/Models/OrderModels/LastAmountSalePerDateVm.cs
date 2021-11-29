using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Common.Models.OrderModels
{
    public class LastAmountSalePerDateVm
    {
        public List<LastAmountSaleItem> Items { get; set; }
    }

    public class LastAmountSaleItem
    {
        public double Sale { get; set; }
        public DateTime Date { get; set; }
    }
}
