using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.OrderItems.Queries.GetPagedOrderItems;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Orders
{
    public class OrderItemListModel : BaseListPage<OrderItem, OrderItemListVm, GetPagedOrderItemsQuery>
    {
        [BindProperty]
        public override DatatableDto Dto { get; set; }
        public OrderItemListModel() : base("created", "desc", bindFilter: "Order.IsPaid")
        {
        }
    }
}
