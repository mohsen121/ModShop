using ModShop.Application.Common.Interfaces;
using ModShop.Common.Util;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Payments
{
    [Datatable("Id", null, null, null, pageTitle: "لیست پرداخت ها ", includes: new[] { "Order.User" })]

    public class PaymentListVm : BaseViewModel<Payment, PaymentListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, isFilterable: false)]
        public Guid Id { get; set; }
        [DatatableNestedColumn]
        public OrderNested Order { get; set; }
        [DatatableColumn("مبلغ", isSortable: true, showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public double Amount { get; set; }
        [DatatableColumn("شماره پیگیری", isSortable: false, showInReport: true, dataType: ColumnDataType.Text, isFilterable: false)]
        public string TraceNumber { get; set; }
        [DatatableColumn("تاریخ پرداخت", isSortable: false, showInReport: true, dataType: ColumnDataType.Date, isFilterable: false)]
        public DateTime? PayTime { get; set; }
        [DatatableColumn("وضعیت پرداخت", isSortable: false, showInReport: true, dataType: ColumnDataType.Text, isFilterable: false)]
        public string Status { get; set; }
        [DatatableColumn("درگاه پرداخت", isSortable: false, showInReport: true, dataType: ColumnDataType.Text, isFilterable: false)]
        public string Gatway { get; set; }

        [DatatableColumn("مشتری", isHidden: true, showInReport: false, dataType: ColumnDataType.Select, filterType: ColumnFilterType.Equality, isFilterable: true, filterSearchUrl: "/Adminstrator/Users/Search")]
        public string UserId { get; set; }

        public override IEnumerable<PaymentListVm> Map(IEnumerable<Payment> source)
        {
            return source.Select(x => new PaymentListVm
            {
                Id = x.Id,
                Order = new OrderNested
                {
                    OrderNumber = x.Order.OrderNumber,
                    UserFullname = $"{x.Order.User.Name} {x.Order.User.Family}",
                },
                Amount = x.Amount,
                TraceNumber = x.TraceNumber,
                PayTime = x.PayTime,
                Status = x.Status.GetDisplayName(),
                Gatway = x.Gatway.GetDisplayName(),
                UserId = x.UserId
            });
        }
    }

    public class OrderNested
    {
        [DatatableColumn("شماره سفارش", name: "order.orderNumber", isSortable: false, showInReport: true, dataType: ColumnDataType.Number, isFilterable: false)]
        public int OrderNumber { get; set; }
        [DatatableColumn("مشتری", name: "order.userFullname", isSortable: false, showInReport: true, dataType: ColumnDataType.Text, isFilterable: false)]
        public string UserFullname { get; set; }

    }
}
