using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Orders.Queries.GetPagedOrders;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Orders
{
    public class OrdersListModel : BaseListPage<Order, OrdersListVm, GetPagedOrdersQuery>
    {
        public OrdersListModel() : base("created", "desc", bindFilter: "IsPaid")
        {
        }

        [BindProperty]
        public override DatatableDto Dto { get; set; }
    }
}
