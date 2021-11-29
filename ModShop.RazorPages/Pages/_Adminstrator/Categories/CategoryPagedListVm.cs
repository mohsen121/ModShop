using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Categories
{
    [Datatable("Id", "/Adminstrator/Categories/New", "/Adminstrator/Categories/Edit", "/Adminstrator/Categories/Delete", excelReportUrl: "/Adminstrator/Categories?handler=Report", pageTitle: "لیست دسته بندی ها", includes: new[] { "Parent" }, searchExpression: "Title.Contains(@0) || Description.Contains(@0)")]
    public class CategoryPagedListVm : BaseViewModel<Category, CategoryPagedListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public Guid Id { get; set; }
        [DatatableColumn("عنوان", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string Title { get; set; }

        [DatatableColumn("زیرمجموعه", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string Parent { get; set; }

        [DatatableColumn("زیرمجموعه", isHidden: true, showInReport: false, dataType: ColumnDataType.Select, filterType: ColumnFilterType.NullableEquality, isFilterable: true, filterSearchUrl: "/Adminstrator/Categories/Search")]
        public string ParentId { get; set; }



        [DatatableColumn("توضیحات", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: false)]
        public string Description { get; set; }
        [DatatableColumn("تاریخ", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }

        public override IEnumerable<CategoryPagedListVm> Map(IEnumerable<Category> source)
        {
            return source.Select(x => new CategoryPagedListVm
            {
                Id = x.Id,
                Title = x.Title,
                Parent = x.Parent?.Title ?? "-",
                Description = x.Description,
                Created = x.Created,
                ParentId = x.ParentId?.ToString()
            }).ToList();
        }
    }
}
