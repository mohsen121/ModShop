using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Sizes
{
    [Datatable("Id", "/Adminstrator/Sizes/New", "/Adminstrator/Sizes/Edit", "/Adminstrator/Sizes/Delete", excelReportUrl: "/Adminstrator/Sizes?handler=Report", pageTitle: "لیست سایزها")]

    public class SizePagedListVm : BaseViewModel<Size, SizePagedListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public Guid Id { get; set; }
        [DatatableColumn("عنوان", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: true)]
        public string Title { get; set; }
        [DatatableColumn("کد", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string Code { get; set; }
        [DatatableColumn("تاریخ", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }

        public override IEnumerable<SizePagedListVm> Map(IEnumerable<Size> source)
        {
            return source.Select(x => new SizePagedListVm
            {
                Id = x.Id,
                Title = x.Title,
                Code = x.Code,
                Created = x.Created
            }).ToList();
        }
    }
}
