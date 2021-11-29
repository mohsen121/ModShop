using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Brands
{
    [Datatable("Id", "/Adminstrator/Brands/New", "/Adminstrator/Brands/Edit", "/Adminstrator/Brands/Delete", excelReportUrl: "/Adminstrator/Brands?handler=Report", pageTitle: "لیست برندها")]
    public class BrandPagedListVm : BaseViewModel<Brand, BrandPagedListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public Guid Id { get; set; }
        [DatatableColumn("عنوان", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string Title { get; set; }
        [DatatableColumn("توضیحات", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string Description { get; set; }
        [DatatableColumn("تاریخ", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }

        public override IEnumerable<BrandPagedListVm> Map(IEnumerable<Brand> source)
        {
            return source.Select(x => new BrandPagedListVm
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Created = x.Created
            }).ToList();
        }
    }
}
