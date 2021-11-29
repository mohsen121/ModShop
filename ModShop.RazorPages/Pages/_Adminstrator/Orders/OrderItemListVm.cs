using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Orders
{
    [Datatable("Id", null, null, null, pageTitle: "لیست ریز سفارشات", includes: new[] { "Product", "Order.User" })]
    public class OrderItemListVm : BaseViewModel<OrderItem, OrderItemListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, isFilterable: false)]
        public Guid Id { get; set; }
        [DatatableNestedColumn]
        public OrderNested Order { get; set; }

        [DatatableNestedColumn]
        public ProductNested Product { get; set; }

        [DatatableColumn("تعداد", showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public double Quantity { get; set; }

        [DatatableColumn("قیمت", showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public double Price { get; set; }
        [DatatableColumn("تخفیف", showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public double Discount { get; set; }
        [DatatableColumn("تاریخ", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }

        [DatatableColumn("محصول", isHidden: true, showInReport: false, dataType: ColumnDataType.Select, filterType: ColumnFilterType.NullableEquality, isFilterable: true, filterSearchUrl: "/Adminstrator/Products/Search")]
        public Guid ProductId { get; set; }
        [DatatableColumn("سفارش", isHidden: true, showInReport: false, dataType: ColumnDataType.Select, filterType: ColumnFilterType.NullableEquality, isFilterable: true, filterSearchUrl: "/Adminstrator/Orders/Search")]
        public Guid OrderId { get; set; }
        public override IEnumerable<OrderItemListVm> Map(IEnumerable<OrderItem> source)
        {
            return source.Select(x => new OrderItemListVm
            {
                Order = new OrderNested
                {
                    OrderNumber = x.Order.OrderNumber,
                    UserFullname = $"{x.Order.User.Name} {x.Order.User.Family}",
                    UserId = x.Order.UserId
                },
                Product = new ProductNested
                {
                    Title = x.Product.Title
                },
                ProductId = x.ProductId,
                Price = x.Price,
                Discount = x.Discount,
                Quantity = x.Quantity,
                Created = x.Created
            });
        }


    }

    public class OrderNested
    {
        [DatatableColumn("شماره سفارش", name: "order.orderNumber", isSortable: false, showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public int OrderNumber { get; set; }
        [DatatableColumn("مشتری", name: "order.userFullname", isSortable: false, showInReport: true, dataType: ColumnDataType.Text, isFilterable: false)]
        public string UserFullname { get; set; }
        [DatatableColumn("مشتری", isHidden: true, name: "order.userId", showInReport: false, dataType: ColumnDataType.Select, filterType: ColumnFilterType.Equality, isFilterable: true, filterSearchUrl: "/Adminstrator/Users/Search")]
        public string UserId { get; set; }
    }

    public class ProductNested
    {
        [DatatableColumn("محصول", name: "product.title", isSortable: false, showInReport: true, dataType: ColumnDataType.Text, isFilterable: false)]
        public string Title { get; set; }
    }
}
