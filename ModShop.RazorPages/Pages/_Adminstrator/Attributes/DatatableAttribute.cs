using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Attributes
{
    public class DatatableAttribute : Attribute
    {
        public DatatableAttribute(string keyName, string createUrl, string editUrl, string deleteUrl, string pageTitle = null, string loadDataUrl = null, string tableId = "kdatatable", string excelReportUrl = null, string pdfReportUrl = null, string[] includes = null, string searchExpression = null, string viewUrl = null)
        {
            KeyName = keyName;
            LoadDataUrl = loadDataUrl;
            CreateUrl = createUrl;
            PageTitle = pageTitle;
            TableId = tableId;
            EditUrl = editUrl;
            DeleteUrl = deleteUrl;
            ExcelReportUrl = excelReportUrl;
            PdfReportUrl = pdfReportUrl;
            Includes = includes;
            SearchExpression = searchExpression;
            ViewUrl = viewUrl;
        }

        public string KeyName { get; }
        public string PageTitle { get; set; }
        public string CreateUrl { get; set; }
        public string LoadDataUrl { get; }
        public string TableId { get; }
        public string EditUrl { get; }
        public string DeleteUrl { get; }
        public string ExcelReportUrl { get; }
        public string PdfReportUrl { get; }
        public string[] Includes { get; set; }
        public string SearchExpression { get; }
        public string ViewUrl { get; set; }
        public bool HasExcelReport { get => !string.IsNullOrEmpty(ExcelReportUrl); }
        public bool HasPdfReport { get => !string.IsNullOrEmpty(PdfReportUrl); }
        public bool HasDefaultLoadDataUrl { get => string.IsNullOrEmpty(LoadDataUrl); }
        public bool IsCreateable { get => !string.IsNullOrEmpty(CreateUrl); }
        public bool IsEditable { get => !string.IsNullOrEmpty(EditUrl); }
        public bool IsDeletable { get => !string.IsNullOrEmpty(DeleteUrl); }
    }

    public class DatatableColumnAttribute : Attribute
    {
        public DatatableColumnAttribute(string title, [CallerMemberName] string name = null, [CallerMemberName] string mapTo = null, bool isSortable = true, bool isHidden = false, bool showInReport = false, bool isFilterable = true, ColumnDataType dataType = ColumnDataType.Text, ColumnFilterType filterType = ColumnFilterType.Equality, string render = null, string filterSearchUrl = null, string filterPrefix = null)
        {
            Title = title;
            Name = name;
            MapTo = mapTo;
            IsSortable = isSortable;
            IsHidden = isHidden;
            ShowInReport = showInReport;
            IsFilterable = isFilterable;
            DataType = dataType;
            FilterType = filterType;
            Render = render;
            FilterSearchUrl = filterSearchUrl;
            FilterPrefix = filterPrefix;
        }

        public string Title { get; }
        public string Name { get; }
        public string MapTo { get; }
        public bool IsSortable { get; }
        public bool IsHidden { get; }
        public bool ShowInReport { get; }
        public bool IsFilterable { get; }
        public ColumnDataType DataType { get; }
        public ColumnFilterType FilterType { get; }
        public string Render { get; }
        public string FilterSearchUrl { get; set; }
        public string FilterPrefix { get; set; }
    }

    public class DatatableIgnoreAttribute : Attribute
    {

    }

    public class DatatableNestedColumn : Attribute
    {
        public DatatableNestedColumn([CallerMemberName] string name = null)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
