using ModShop.Application.Common.Interfaces;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Reports
{
    [Datatable("Id", null, null, null, pageTitle: "گزارش فروش به تفکیک محصول", includes: new string[] { "Items.Product" })]
    public class ProductSepratedListVm : BaseViewModel<ProductSepratedItemVm, ProductSepratedListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public Guid Id { get; set; }

        [DatatableColumn("عنوان", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: true, isSortable: true, filterPrefix: "Items.Product.")]
        public string Title { get; set; }

        [DatatableColumn("تصویر", showInReport: false, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false, isSortable: false, render: "<img class=\"w-65px rounded-sm\" src=\"{data}\">")]
        public string Image { get; set; }

        [DatatableColumn("تعداد", showInReport: true, dataType: ColumnDataType.Number, filterType: ColumnFilterType.Containing, isFilterable: false, isSortable: true, render: "<span class=\"badge badge-primary\">{data}</span>")]
        public int Quantity { get; set; }

        [DatatableColumn("تاریخ", isHidden: true, showInReport: false, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }

        public override IEnumerable<ProductSepratedListVm> Map(IEnumerable<ProductSepratedItemVm> source)
        {
            return source.Select(x => new ProductSepratedListVm
            {
                Id = x.Product.Id,
                Created = x.Product.Created,
                Title = x.Product.Title,
                Image = x.Product.Image,
                Quantity = x.Quantity
            });
        }
    }
}
