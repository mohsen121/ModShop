using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.ViewComponents
{
    public class DatatableDto
    {
        public DatatableDto()
        {
            Columns = new List<DatatableColumnDto>();
        }
        public string PageTitle { get; set; }
        public string KeyName { get; set; }
        public string TableId { get; set; }
        public string EditUrl { get; set; }
        public string CreateUrl { get; set; }
        public string DeleteUrl { get; set; }
        public string LoadDataUrl { get; set; }
        public string ExcelReportUrl { get; set; }
        public string PdfReportUrl { get; set; }
        public string[] Includes { get; set; }
        public bool HasExcelReport { get => !string.IsNullOrEmpty(ExcelReportUrl); }
        public bool HasPdfReport { get => !string.IsNullOrEmpty(PdfReportUrl); }
        public bool HasDefaultLoadDataUrl { get => string.IsNullOrEmpty(LoadDataUrl); }
        public bool IsCreateable { get => !string.IsNullOrEmpty(CreateUrl); }
        public bool IsEditable { get => !string.IsNullOrEmpty(EditUrl); }
        public bool IsDeletable { get => !string.IsNullOrEmpty(DeleteUrl); }
        public bool IsViewable { get => !string.IsNullOrEmpty(ViewUrl); }

        public List<DatatableColumnDto> Columns { get; set; }
        public string ViewUrl { get; set; }
    }

    public class DatatableColumnDto
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public bool IsSortable { get; set; }
        public bool IsFilterable { get; set; }
        public bool IsHidden { get; set; }
        public bool IsNested { get; set; }
        public bool ShowInReport { get; set; }
        public string Render { get; set; }
        public string MapTo { get; set; }
        public string FilterSearchUrl { get; set; }
        public ColumnDataType DataType { get; set; }
        public ColumnFilterType FilterType { get; set; }

        public List<DatatableColumnDto> Columns { get; set; }
        public string FilterPrefix { get; set; }
    }
}
