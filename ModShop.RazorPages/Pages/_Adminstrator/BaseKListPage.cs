using AutoMapper.QueryableExtensions;
using AutoMapper.QueryableExtensions.Impl;
using ClosedXML.Excel;
using HtmlAgilityPack;
using ModShop.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;

namespace ModShop.RazorPages.Pages._Adminstrator
{
    public abstract class BaseKListPage<TEntity, TViewModel, TPagedQuery, TTotalCountQuery> : BaseAdminstratorPage
        where TPagedQuery : PagedList<TEntity>, new()
        where TTotalCountQuery : TotalCountQuery<TEntity>, new()
        where TViewModel : BaseViewModel<TEntity, TViewModel>, new()
    {
        private string _sortColumn;
        private string _sortColumnDir;

        [BindProperty]
        public virtual DatatableDto Dto { get; set; }
        public BaseKListPage(string sortColumn, string sortColumnDir)
        {

            _sortColumn = sortColumn;
            _sortColumnDir = sortColumnDir;
        }

        public virtual IActionResult OnGet()
        {
            Dto = BuildDatatableDto(new TViewModel());
            return Page();
        }

        protected virtual DatatableDto BuildDatatableDto(TViewModel viewModel)
        {

            var datatableAttr = viewModel.GetType().GetCustomAttribute<DatatableAttribute>();
            var dto = new DatatableDto()
            {
                KeyName = datatableAttr.KeyName,
                DeleteUrl = datatableAttr.DeleteUrl,
                EditUrl = datatableAttr.EditUrl,
                TableId = datatableAttr.TableId,
                ExcelReportUrl = datatableAttr.ExcelReportUrl,
                LoadDataUrl = datatableAttr.LoadDataUrl,
                PdfReportUrl = datatableAttr.PdfReportUrl
            };

            var fields = viewModel.GetType().GetProperties();

            foreach (var f in fields)
            {
                var colAttr = f.GetCustomAttribute<DatatableColumnAttribute>();
                dto.Columns.Add(new DatatableColumnDto
                {
                    Name = colAttr.Name,
                    Title = colAttr.Title,
                    IsHidden = colAttr.IsHidden,
                    IsSortable = colAttr.IsSortable,
                    ShowInReport = colAttr.ShowInReport,
                });
            }

            return dto;
        }

        public virtual async Task<ActionResult> OnPostLoadData()
        {

            var form = Request.Form;
            var start = form["pagination[page]"].FirstOrDefault();
            var length = form["pagination[perpage]"].FirstOrDefault();
            var search = form["query[generalSearch]"].FirstOrDefault();

            try
            {
                var temp = form["sort[field]"].FirstOrDefault();
                if (!string.IsNullOrEmpty(temp))
                    _sortColumn = temp;
            }
            catch { }


            try
            {
                var temp = form["sort[sort]"].FirstOrDefault();
                if (!string.IsNullOrEmpty(temp))
                    _sortColumnDir = temp;
            }
            catch { }


            _ = int.TryParse(start, out var pageStart);
            _ = int.TryParse(length, out var pageLength);

            var pagedQuery = new TPagedQuery()
            {
                Start = pageStart,
                Length = pageLength,
                OrderColumn = _sortColumn,
                OrderDir = _sortColumnDir,
                Search = search
            };

            var queryResult = await Mediator.Send(pagedQuery);

            var data = new TViewModel().Map(queryResult)
                ;

            var totalCountQuery = new TTotalCountQuery();
            var totalCountResult = await Mediator.Send(totalCountQuery);

            return new JsonResult(new
            {
                data = data,
                meta = new
                {
                    field = _sortColumn,
                    page = pageStart,
                    total = totalCountResult,
                    sort = _sortColumnDir,
                    perpage = length
                }
            });
        }

        public virtual async Task<IActionResult> OnPostReport(string reportType = "excel")
        {
            var pagedQuery = new TPagedQuery()
            {
                Start = 1,
                Length = int.MaxValue,
                OrderColumn = _sortColumn,
                OrderDir = _sortColumnDir,
                Search = null
            };

            var query = await Mediator.Send(pagedQuery);
            var data = new TViewModel().Map(query);

            Dto = BuildDatatableDto(new TViewModel());

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");
                var currentRow = 1;
                //worksheet.Cell(currentRow, 1).Value = "Id";
                //worksheet.Cell(currentRow, 2).Value = "Username";
                for (var i = 0; i < Dto.Columns.Count; i++)
                {
                    if (Dto.Columns[i].ShowInReport)
                        worksheet.Cell(1, i + 1).Value = Dto.Columns[i].Title ?? Dto.Columns[i].Name;
                }

                foreach (var record in data)
                {
                    currentRow++;
                    for (var i = 0; i < Dto.Columns.Count; i++)
                    {

                        worksheet.Cell(currentRow, i + 1).Value = record.GetType().GetProperty(Dto.Columns[i].Name).GetValue(record, null);

                    }


                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "report.xlsx");
                }
            }
            //var table = $"<table class='table-borderd table-striped' id='{Dto.TableId}' style='display:none'><thead><tr>";
            //foreach(var col in Dto.Columns)
            //{
            //    if (col.ShowInReport)
            //        table += $"<th>{col.Title}</th>";
            //}
            //table += $"</tr></thead>";

            //foreach(var record in data)
            //{
            //    table += $"<tr>";
            //    foreach (var col in Dto.Columns.Where(x => x.ShowInReport))
            //    {
            //        var value = record.GetType().GetProperty(col.Name).GetValue(record, null);
            //        table += $"<td>{value}</td>";
            //    }
            //    table += "</tr>";
            //}
            //table += $"</table>";

            //var fileName = "output.xls";
            //var mimeType = "application/excel";
            //var fileBytes = Encoding.UTF8.GetBytes(table);

            //return new FileContentResult(fileBytes, mimeType)
            //{
            //    FileDownloadName = fileName
            //};
        }
    }
}
