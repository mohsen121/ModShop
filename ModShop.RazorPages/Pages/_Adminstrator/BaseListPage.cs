using ModShop.Application.Common.Interfaces;
using ModShop.Common.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ModShop.Application.BaseEntities.Queries.GetPagedBaseEntities;
using ModShop.Domain;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.Application.BaseEntities.Queries.GetAllBaseEntitiesCount;
using ClosedXML;

namespace ModShop.RazorPages.Pages._Adminstrator
{
    public class BaseListPage<TEntity, TViewModel, TPagedQuery, TTotalCountQuery> : BaseListPage<TEntity, TViewModel, TPagedQuery>
        where TPagedQuery : IGetPagedBaseEntitiesQuery<TEntity>, new()
        where TViewModel : BaseViewModel<TEntity, TViewModel>, new()
        where TEntity : BaseEntity, new()
    {
        public BaseListPage(string sortColumn, string sortColumnDir) : base(sortColumn, sortColumnDir)
        {
        }
    }

    public class BaseListPage<TEntity, TViewModel, TPagedQuery> : BaseAdminstratorPage
        where TPagedQuery : IGetPagedBaseEntitiesQuery<TEntity>, new()
        where TViewModel : BaseViewModel<TEntity, TViewModel>, new()
        where TEntity : BaseEntity, new()
    {
        private string _sortColumn;
        private string _sortColumnDir;
        private string _bindFilter;

        [BindProperty]
        public virtual DatatableDto Dto { get; set; }
        public BaseListPage(string sortColumn, string sortColumnDir, string bindFilter = null)
        {

            _sortColumn = sortColumn;
            _sortColumnDir = sortColumnDir;
            _bindFilter = bindFilter;
        }

        public virtual IActionResult OnGet()
        {
            Dto = BuildDatatableDto();
            return Page();
        }

        protected virtual DatatableDto BuildDatatableDto()
        {
            var viewModel = new TViewModel();
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
                ViewUrl = datatableAttr.ViewUrl
            };

            var fields = viewModel.GetType().GetProperties().Where(x => !x.HasAttribute<DatatableIgnoreAttribute>());

            foreach (var f in fields)
            {
                var nestedAttr = f.GetCustomAttribute<DatatableNestedColumn>();
                if (nestedAttr != null)
                {

                    var type = f.PropertyType;
                    var nestedFields = type.GetProperties().Where(x => !x.HasAttribute<DatatableIgnoreAttribute>());
                    var nestedCol = new DatatableColumnDto
                    {
                        Name = nestedAttr.Name,
                        IsNested = true,
                        Columns = new List<DatatableColumnDto>()
                    };

                    foreach (var nf in nestedFields)
                    {
                        var dataColAttr = nf.GetCustomAttribute<DatatableColumnAttribute>();
                        nestedCol.Columns.Add(new DatatableColumnDto
                        {
                            Name = dataColAttr.Name.ToCamelCase(),
                            Title = dataColAttr.Title,
                            IsHidden = dataColAttr.IsHidden,
                            IsSortable = dataColAttr.IsSortable,
                            ShowInReport = dataColAttr.ShowInReport,
                            DataType = dataColAttr.DataType,
                            FilterType = dataColAttr.FilterType,
                            IsFilterable = dataColAttr.IsFilterable,
                            Render = dataColAttr.Render,
                            MapTo = dataColAttr.MapTo,
                            FilterSearchUrl = dataColAttr.FilterSearchUrl,
                        });
                    }

                    dto.Columns.Add(nestedCol);
                }
                else
                {
                    var colAttr = f.GetCustomAttribute<DatatableColumnAttribute>();
                    if (colAttr != null)
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
                        });
                }

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
                    _sortColumn = temp;
            }
            catch { }


            try
            {
                var temp = form["order[0][dir]"].FirstOrDefault();
                if (!string.IsNullOrEmpty(temp))
                    _sortColumnDir = temp;
            }
            catch { }

            // filters
            var filters = new Dictionary<string, object[]>();
            for (var i = 0; i < Dto.Columns.Where(x => x.IsFilterable).Count(); i++)
            {

                var key = form[$"filters[{i}][key]"].FirstOrDefault();
                var value = form[$"filters[{i}][value]"].FirstOrDefault();
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    filters.Add(value, new[] { value });
                //var isSearchable = form[$"columns[{i}][searchable]"].FirstOrDefault();
                //if (!string.IsNullOrEmpty(isSearchable))
                //{
                //    _ = bool.TryParse(isSearchable, out var isSearchableTemp);
                //    if (isSearchableTemp)
                //    {
                //        var searchKey = form[$"columns[{i}][data]"].FirstOrDefault();
                //        var searchValue = form[$"columns[{i}][search][value]"].FirstOrDefault();
                //        if (!string.IsNullOrEmpty(searchKey) && !string.IsNullOrEmpty(searchValue))
                //        {
                //            var searchArr = searchValue.Split("|", StringSplitOptions.RemoveEmptyEntries);
                //            filters.Add($"{Dto.Columns[i].FilterType.GetDisplayPrompt().Replace("{name}", Dto.Columns[i].MapTo)}", searchArr);
                //        }
                //    }

                //}
            }

            if (!string.IsNullOrEmpty(_bindFilter))
            {
                filters.Add(_bindFilter, new string[] { });
            }

            _ = int.TryParse(start, out var pageStart);
            _ = int.TryParse(length, out var pageLength);

            var pagedQuery = new TPagedQuery()
            {
                Start = pageStart,
                Length = pageLength,
                OrderColumn = _sortColumn,
                OrderDir = _sortColumnDir,
                Search = search,
                Filters = filters,
                Includes = Dto.Includes
            };

            var queryResult = await Mediator.Send(pagedQuery);

            var data = new TViewModel().Map(queryResult.Item1)
                ;

            //var totalCountQuery = new TTotalCountQuery();
            //var totalCountResult = await Mediator.Send(totalCountQuery);

            return new JsonResult(new
            {
                draw = draw,
                recordsFiltered = queryResult.Item2,
                recordsTotal = queryResult.Item2,
                data = data
            });
        }
    }
}
