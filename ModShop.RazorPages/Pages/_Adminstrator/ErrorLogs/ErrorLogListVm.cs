using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.ErrorLogs
{
    [Datatable("Id", null, null, null, excelReportUrl: "/Adminstrator/ErrorLogs?handler=Report", pageTitle: "لیست خطاها")]
    public class ErrorLogListVm : BaseViewModel<ErrorLog, ErrorLogListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public Guid Id { get; set; }

        [DatatableColumn("کلاس", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string ClassName { get; set; }

        [DatatableColumn("متد", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string MethodName { get; set; }

        [DatatableColumn("خطا", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string Error { get; set; }






        [DatatableColumn("تاریخ", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: false)]
        public DateTime Created { get; set; }

        public override IEnumerable<ErrorLogListVm> Map(IEnumerable<ErrorLog> source)
        {
            return source.Select(x => new ErrorLogListVm
            {
                Id = x.Id,
                ClassName = x.ClassName,
                MethodName = x.MethodName,
                Error = x.Error,
                Created = x.Created
            }).ToList();
        }
    }
}
