using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Common.Util;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System.Linq.Dynamic.Core;

namespace ModShop.RazorPages.Pages._Adminstrator.Users
{
    public class AdminListModel : UserListModel
    {
        private UserManager<User> _userManager;

        public AdminListModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override DatatableDto BuildDatatableDto()
        {
            var viewModel = new UserListVm();
            var datatableAttr = viewModel.GetType().GetCustomAttribute<DatatableAttribute>();
            var dto = new DatatableDto()
            {
                KeyName = datatableAttr.KeyName,
                DeleteUrl = null,
                EditUrl = "/Adminstrator/Admins/Edit",
                CreateUrl = "/Adminstrator/Admins/New",
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

        protected override async Task<Tuple<List<User>, int>> GetQueryResult(int pageStart, int pageLength, string search, Dictionary<string, string[]> filters = null)
        {
            var query = (await _userManager.GetUsersInRoleAsync("Admin")).AsQueryable();

            if (string.IsNullOrEmpty(_sortColumnDir))
            {
                if (!string.IsNullOrEmpty(_sortColumn))
                    query = query.OrderBy(_sortColumn, "asc");
                else
                {
                    query = query.OrderByDescending(x => x.Created);
                }
            }
            else
            {
                query = query.OrderBy(_sortColumn, _sortColumnDir);
            }

            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                    query = query.Where(filter.Key);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search) || x.Email.Contains(search));
            }

            return new Tuple<List<User>, int>(query.ToList(), query.Count());
        }
    }
}
