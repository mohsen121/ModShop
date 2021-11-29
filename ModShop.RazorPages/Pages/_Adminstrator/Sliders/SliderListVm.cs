using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Sliders
{
    [Datatable("Id", "/Adminstrator/Sliders/New", "/Adminstrator/Sliders/Edit", "/Adminstrator/Sliders/Delete", excelReportUrl: "/Adminstrator/Sliders?handler=Report", pageTitle: "لیست اسلایدرها")]
    public class SliderListVm : BaseViewModel<Slider, SliderListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public Guid Id { get; set; }

        [DatatableColumn("محتوای html", showInReport: false, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false, render: "<pre>{data}</pre>")]
        public string HtmlText { get; set; }

        [DatatableColumn("تصویر", showInReport: false, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false, isSortable: false, render: "<img class=\"w-65px rounded-sm\" src=\"{data}\">")]
        public string Image { get; set; }
        [DatatableColumn("تاریخ", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }
        public override IEnumerable<SliderListVm> Map(IEnumerable<Slider> source)
        {
            return source.Select(x => new SliderListVm
            {
                Id = x.Id,
                Image = x.Image,
                HtmlText = x.Image,
                Created = x.Created
            });
        }
    }
}
