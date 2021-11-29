using AutoMapper;
using ModShop.Application.Common.Interfaces;
using ModShop.Application.Common.Mappings;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Products
{
    [Datatable("Id", "/Adminstrator/Products/New", "/Adminstrator/Products/Edit", "/Adminstrator/Products/Delete", excelReportUrl: "/Adminstrator/Products?handler=Report", pageTitle: "لیست محصولات")]
    public class ProductPageListVm : BaseViewModel<Product, ProductPageListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public Guid Id { get; set; }

        [DatatableColumn("عنوان", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: true)]
        public string Title { get; set; }

        [DatatableColumn("توضیحات", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string Description { get; set; }

        [DatatableColumn("تصویر", showInReport: false, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false, isSortable: false, render: "<img class=\"w-65px rounded-sm\" src=\"{data}\">")]
        public string Image { get; set; }

        [DatatableColumn("تعداد", showInReport: true, dataType: ColumnDataType.Number, filterType: ColumnFilterType.Containing, isFilterable: false, isSortable: true, render: "<span class=\"badge badge-primary\">{data}</span>")]
        public int Quantity { get; set; }


        [DatatableColumn("تاریخ", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }

        public override IEnumerable<ProductPageListVm> Map(IEnumerable<Product> source)
        {
            return source.Select(x => new ProductPageListVm
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Image = x.Image,
                Quantity = x.Quantity,
                Created = x.Created
            }).ToList();
        }
    }
}
