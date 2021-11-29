using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Users.Queries.GetAllUsersCount;
using ModShop.Application.Users.Queries.GetUsersPaged;
using ModShop.Common.Util;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Users
{

    public class UserListModel : BaseAdminstratorPage
    {
        protected string _sortColumn;
        protected string _sortColumnDir;
        private string _pageTitle;

        public UserListModel()
        {

            _sortColumn = "id";
            _sortColumnDir = "desc";
        }

        //public UserListModel(string sortColumn, string sortColumnDir, string pageTitle)
        //{

        //    _sortColumn = sortColumn;
        //    _sortColumnDir = sortColumnDir;
        //    _pageTitle = pageTitle;
        //}



        public IActionResult OnGet()
        {
            Dto = BuildDatatableDto();
            return Page();
        }

        [BindProperty]
        public virtual DatatableDto Dto { get; set; }

        protected virtual DatatableDto BuildDatatableDto()
        {
            var viewModel = new UserListVm();
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
                Includes = datatableAttr.Includes
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

            var filters = new Dictionary<string, string[]>();
            for (var i = 0; i < 3; i++)
            {

                var isSearchable = form[$"columns[{i}][searchable]"].FirstOrDefault();
                if (!string.IsNullOrEmpty(isSearchable))
                {
                    _ = bool.TryParse(isSearchable, out var isSearchableTemp);
                    if (isSearchableTemp)
                    {
                        var searchKey = form[$"columns[{i}][data]"].FirstOrDefault();
                        var searchValue = form[$"columns[{i}][search][value]"].FirstOrDefault();
                        if (!string.IsNullOrEmpty(searchKey) && !string.IsNullOrEmpty(searchValue))
                        {
                            var searchArr = searchValue.Split("|", StringSplitOptions.RemoveEmptyEntries);
                            filters.Add($"{Dto.Columns[i].FilterType.GetDisplayPrompt().Replace("{name}", Dto.Columns[i].MapTo)}", searchArr);
                        }
                    }

                }
            }

            _ = int.TryParse(start, out var pageStart);
            _ = int.TryParse(length, out var pageLength);

            var queryResult = await GetQueryResult(pageStart, pageLength, search, filters);

            var data = new UserListVm().Map(queryResult.Item1);

            return new JsonResult(new
            {
                draw = draw,
                recordsFiltered = queryResult.Item2,
                recordsTotal = queryResult.Item2,
                data = data
            });
        }

        protected virtual Task<Tuple<List<User>, int>> GetQueryResult(int pageStart, int pageLength, string search, Dictionary<string, string[]> filters = null)
        {
            var pagedQuery = new GetUsersPagedQuery()
            {
                Start = pageStart,
                Length = pageLength,
                OrderColumn = _sortColumn,
                OrderDir = _sortColumnDir,
                Search = search,
                Filters = filters,
                Includes = Dto.Includes
            };

            return Mediator.Send(pagedQuery);
        }
    }
}
