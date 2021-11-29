using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Orders.Queries.GetPagedOrders;
using ModShop.Common.Util;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System.Linq.Dynamic.Core;

namespace ModShop.RazorPages.Pages._Adminstrator.Reports
{
    public class ProductSepratedSaleReportModel : BaseAdminstratorPage
    {
        private string _sortColumn;
        private string _sortColumnDir;
        private string _pageTitle;

        public ProductSepratedSaleReportModel()
        {

            _sortColumn = "id";
            _sortColumnDir = "desc";
        }

        public IActionResult OnGet()
        {
            Dto = BuildDatatableDto();
            return Page();
        }

        [BindProperty]
        public virtual DatatableDto Dto { get; set; }

        protected virtual DatatableDto BuildDatatableDto()
        {
            var viewModel = new ProductSepratedListVm();
            var datatableAttr = viewModel.GetType().GetCustomAttribute<DatatableAttribute>();
            var dto = new DatatableDto()
            {
                KeyName = datatableAttr.KeyName,
                DeleteUrl = datatableAttr.DeleteUrl,
                EditUrl = datatableAttr.EditUrl,
                CreateUrl = datatableAttr.CreateUrl,
                PageTitle = datatableAttr.PageTitle,
                TableId = datatableAttr.TableId,
                ExcelReportUrl = datatableAttr.ExcelReportUrl,
                LoadDataUrl = datatableAttr.LoadDataUrl,
                PdfReportUrl = datatableAttr.PdfReportUrl,
                Includes = datatableAttr.Includes,
            };

            var fields = viewModel.GetType().GetProperties();

            foreach (var f in fields)
            {
                var colAttr = f.GetCustomAttribute<DatatableColumnAttribute>();
                dto.Columns.Add(new DatatableColumnDto
                {
                    Name = colAttr.Name.ToCamelCase(),
                    Title = colAttr.Title,
                    IsHidden = colAttr.IsHidden,
                    IsSortable = colAttr.IsSortable,
                    ShowInReport = colAttr.ShowInReport,
                    DataType = colAttr.DataType,
                    FilterType = colAttr.FilterType,
                    IsFilterable = colAttr.IsFilterable,
                    Render = colAttr.Render,
                    MapTo = colAttr.MapTo,
                    FilterSearchUrl = colAttr.FilterSearchUrl,
                    FilterPrefix = colAttr.FilterPrefix
                });
            }

            return dto;
        }

        public virtual async Task<IActionResult> OnPostLoadData()
        {
            var form = Request.Form;
            var draw = form["draw"].FirstOrDefault();
            var start = form["start"].FirstOrDefault();
            var length = form["length"].FirstOrDefault();
            var search = form["search[value]"].FirstOrDefault();

            Dto = BuildDatatableDto();

            try
            {
                var temp = form["columns[" + form["order[0][column]"].FirstOrDefault() +
                                    "][name]"].FirstOrDefault();
                if (!string.IsNullOrEmpty(temp))
                {
                    var col = Dto.Columns.FirstOrDefault(x => x.Name.ToLower() == temp.ToLower());
                    if (col != null)
                    {
                        if (!string.IsNullOrEmpty(col.FilterPrefix))
                        {
                            temp = col.FilterPrefix + temp;
                        }
                    }
                    _sortColumn = temp;
                }

            }
            catch { }


            try
            {
                var temp = form["order[0][dir]"].FirstOrDefault();
                if (!string.IsNullOrEmpty(temp))
                    _sortColumnDir = temp;
            }
            catch { }

            var filters = new Dictionary<string, string>();
            for (var i = 0; i < Dto.Columns.Where(x => x.IsFilterable).Count(); i++)
            {

                var key = form[$"filters[{i}][key]"].FirstOrDefault();
                var value = form[$"filters[{i}][value]"].FirstOrDefault();
                var valueWithoutExpression = form[$"filters[{i}][valueWithoutExpression]"].FirstOrDefault();
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    if (key.ToLower() == "title")
                        filters.Add(key, valueWithoutExpression);
                    else
                        filters.Add(key, value);
                }

            }

            _ = int.TryParse(start, out var pageStart);
            _ = int.TryParse(length, out var pageLength);

            var queryResult = await GetQueryResult(pageStart, pageLength, search, filters);

            var data = new ProductSepratedListVm().Map(queryResult);

            return new JsonResult(new
            {
                draw = draw,
                recordsFiltered = data.Count(),
                recordsTotal = data.Count(),
                data = data
            });
        }

        protected virtual async Task<List<ProductSepratedItemVm>> GetQueryResult(int pageStart, int pageLength, string search, Dictionary<string, string> filters = null)
        {
            var pagedQuery = new GetPagedOrdersQuery()
            {
                Start = 0,
                Length = int.MaxValue,
                OrderColumn = _sortColumn,
                OrderDir = _sortColumnDir,
                Search = search,

                Includes = new string[] { "Items.Product" }
            };

            var result = await Mediator.Send(pagedQuery);

            var list = result.Item1.Where(x => x.IsPaid).ToList();
            var productTitleFilter = string.Empty;
            foreach (var f in filters)
            {
                if (f.Key.ToLower() == "title")
                {
                    productTitleFilter = f.Value;
                }
                else
                {
                    list = list.AsQueryable().Where(f.Value).ToList();
                }
            }
            var topSaleProducts = list

                .SelectMany(x => x.Items)
                .GroupBy(x => x.ProductId)
                .Select(x => new ProductSepratedItemVm
                {
                    Product = x.Select(a => new Product
                    {
                        Id = a.ProductId,
                        Title = a.Product.Title,
                        Created = a.Created,
                        Image = a.Product.Image,
                        Quantity = a.Quantity,

                    }).FirstOrDefault(),
                    Quantity = x.Sum(a => a.Quantity)
                })
                .ToList();

            if (!string.IsNullOrEmpty(productTitleFilter))
                topSaleProducts = topSaleProducts.Where(x => x.Product.Title.Contains(productTitleFilter)).ToList();

            return topSaleProducts.Skip(pageStart).Take(pageLength).ToList();
        }
    }
}
