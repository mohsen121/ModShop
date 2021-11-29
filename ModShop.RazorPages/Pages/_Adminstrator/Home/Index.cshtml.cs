using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Common.Models.OrderModels;
using ModShop.Application.Orders.Queries.GetLastGroupBySales;
using ModShop.Application.Orders.Queries.GetPagedOrders;
using ModShop.Domain.Entities;

namespace ModShop.RazorPages.Pages._Adminstrator.Home
{
    public class IndexModel : BaseAdminstratorPage
    {
        [BindProperty]
        public LastSalesPerDateVm LastSaleChartVm { get; set; }

        [BindProperty]
        public LastAmountSalePerDateVm LastAmountSaleChartVm { get; set; }

        [BindProperty]
        public TopSaleProductVm TopSaleProductsChartVm { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //var lastQuery = new GetLastGroupBySalesQuery();
            //LastSaleChartVm = new LastSalesPerDateVm
            //{
            //    Items = await Mediator.Send(lastQuery)
            //};
            var query = new GetPagedOrdersQuery
            {
                Includes = new string[] { "Items.Product" },
                Length = int.MaxValue,
                Start = 0,
                Filters = new Dictionary<string, object[]>
                {
                    {$"Created >= \"{DateTime.Now.AddDays(-30)}\" && IsPaid", new string[]{ } }
                }
            };
            var items = await Mediator.Send(query);
            InitilizeCountSaleChart(items.Item1);
            InitilizeAmountSaleChart(items.Item1);
            InitilizeTopSaleProducts(items.Item1);

            return Page();
        }

        private void InitilizeTopSaleProducts(List<Order> items)
        {
            var topSaleProducts = items

                .SelectMany(x => x.Items)
                .GroupBy(x => x.ProductId)
                .Select(x => new TopSaleProductItem { Title = x.FirstOrDefault()?.Product?.Title, SaleCount = x.Sum(a => a.Quantity) })
                .ToList();

            TopSaleProductsChartVm = new TopSaleProductVm
            {
                Products = topSaleProducts.OrderByDescending(x => x.SaleCount).Take(5).ToList()
            };
        }

        private void InitilizeAmountSaleChart(List<Order> items)
        {
            var countSales = items

                .GroupBy(x => x.Created.Date)
                .Select(x => new LastAmountSaleItem { Date = x.Key, Sale = x.Sum(a => a.TotalPrice) })
                .ToList();

            var lastMonth = DateTime.Now.AddDays(-30);
            var today = DateTime.Now;
            for (var day = lastMonth.Date; day.Date <= today.Date; day = day.AddDays(1))
            {
                if (countSales.Any(x => x.Date.Date == day.Date))
                    continue;

                countSales.Add(new LastAmountSaleItem
                {
                    Date = day.Date,
                    Sale = 0
                });
            }

            LastAmountSaleChartVm = new LastAmountSalePerDateVm
            {
                Items = countSales.OrderBy(x => x.Date).ToList()
            };
        }

        private void InitilizeCountSaleChart(List<Order> items)
        {
            var countSales = items
                .Select(x => new
                {
                    x.Created
                })
                .GroupBy(x => x.Created.Date)
                .Select(x => new LastSaleItem { Date = x.Key, SaleCount = x.Count() })
                .ToList();

            var lastMonth = DateTime.Now.AddDays(-30);
            var today = DateTime.Now;
            for (var day = lastMonth.Date; day.Date <= today.Date; day = day.AddDays(1))
            {
                if (countSales.Any(x => x.Date.Date == day.Date))
                    continue;

                countSales.Add(new LastSaleItem
                {
                    Date = day.Date,
                    SaleCount = 0
                });
            }

            LastSaleChartVm = new LastSalesPerDateVm
            {
                Items = countSales.OrderBy(x => x.Date).ToList()
            };
        }
    }
}
