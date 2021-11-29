using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.ErrorLogs.Queries.GetAllErrorLogsCount;
using ModShop.Application.ErrorLogs.Queries.GetPagedErrorLogs;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.ErrorLogs
{
    public class ErrorLogListModel : BaseListPage<ErrorLog, ErrorLogListVm, GetPagedErrorLogsQuery, GetAllErrorLogsCountQuery>
    {
        [BindProperty]
        public override DatatableDto Dto { get; set; }

        public ErrorLogListModel() : base("id", "desc")
        {

        }
    }
}
