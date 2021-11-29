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
    [Datatable("Id", null, null, null, excelReportUrl: null, pageTitle: "لیست سفارشات", includes: new[] { "User" }, viewUrl: "/Adminstrator/Orders")]
    public class OrdersListVm : BaseViewModel<Order, OrdersListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: false, isFilterable: false)]
        public Guid Id { get; set; }
        [DatatableColumn("کاربر", showInReport: true, dataType: ColumnDataType.Text, isFilterable: false)]
        public string User { get; set; }


        [DatatableColumn("شماره سفارش", showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public int OrderNumber { get; set; }
        [DatatableColumn("قیمت کل", showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public double TotalPrice { get; set; }

        [DatatableColumn("تخفیف", showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public double Discount { get; set; }
        [DatatableColumn("مالیات", showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public double Tax { get; set; }
        [DatatableColumn("تاریخ", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }

        [DatatableColumn("کاربر", isHidden: true, showInReport: false, dataType: ColumnDataType.Select, filterType: ColumnFilterType.Equality, isFilterable: true, filterSearchUrl: "/Adminstrator/Users/Search", isSortable: false)]
        public string UserId { get; set; }

        public override IEnumerable<OrdersListVm> Map(IEnumerable<Order> source)
        {
            return source.Select(x => new OrdersListVm
            {
                Id = x.Id,
                User = string.Concat(x.User.Name, " ", x.User.Family),
                TotalPrice = x.TotalPrice,
                Discount = x.Discount,
                Tax = x.Tax,
                Created = x.Created,
                UserId = x.UserId,
                OrderNumber = x.OrderNumber
            });
        }
    }
}
