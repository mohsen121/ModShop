using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Colors
{
    [Datatable("Id", "/Adminstrator/Colors/New", "/Adminstrator/Colors/Edit", "/Adminstrator/Colors/Delete", excelReportUrl: "/Adminstrator/Colors?handler=Report", pageTitle: "لیست رنگ ها")]
    public class ColorPagedListVm : BaseViewModel<Color, ColorPagedListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public Guid Id { get; set; }
        [DatatableColumn("عنوان", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string Title { get; set; }
        [DatatableColumn("رنگ", showInReport: false, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false, render: "<div style=\"width: 20px;height: 20px;margin: 5px;border: 1px solid rgba(0, 0, 0, .2);background: #{data};\"></div>", mapTo: "HexCode")]
        public string Color { get; set; }

        [DatatableColumn("کد رنگ", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false, render: "{data}", mapTo: "HexCode")]
        public string Hex { get; set; }

        [DatatableColumn("تاریخ", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }

        public override IEnumerable<ColorPagedListVm> Map(IEnumerable<Color> source)
        {
            return source.Select(x => new ColorPagedListVm
            {
                Id = x.Id,
                Title = x.Title,
                Color = x.HexCode,
                Hex = x.HexCode,
                Created = x.Created
            });
        }
    }
}
