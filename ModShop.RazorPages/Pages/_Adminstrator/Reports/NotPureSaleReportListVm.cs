using ModShop.Application.Common.Interfaces;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Reports
{
    [Datatable("Id", null, null, null, pageTitle: "گزارش فروش ناخالص", includes: new string[] { "Items.Product" })]
    public class NotPureSaleReportListVm : BaseViewModel<NotPureSaleReportListVm, NotPureSaleReportListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public Guid Id { get; set; }
        [DatatableColumn("مجموع فروش ناخالص", showInReport: true, dataType: ColumnDataType.Number, filterType: ColumnFilterType.Containing, isFilterable: false, isSortable: true, render: "numberWithCommas")]
        public double TotalPrice { get; set; }
        [DatatableColumn("مجموع مالیات", showInReport: true, dataType: ColumnDataType.Number, filterType: ColumnFilterType.Containing, isFilterable: false, isSortable: true, render: "numberWithCommas")]
        public double TotalTax { get; set; }
        [DatatableColumn("مجموع تخفیف", showInReport: true, dataType: ColumnDataType.Number, filterType: ColumnFilterType.Containing, isFilterable: false, isSortable: true, render: "numberWithCommas")]
        public double TotalDiscount { get; set; }
        [DatatableColumn("تاریخ", isHidden: true, showInReport: false, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }
        public override IEnumerable<NotPureSaleReportListVm> Map(IEnumerable<NotPureSaleReportListVm> source)
        {
            return source;
        }
    }
}
