using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Attributes;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.Users
{
    [Datatable("Id", null, null, null, pageTitle: "لیست کاربران")]
    public class UserListVm : BaseViewModel<User, UserListVm>
    {
        [DatatableColumn("شناسه", isHidden: true, showInReport: true, isFilterable: false)]
        public string Id { get; set; }
        [DatatableColumn("نام", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: true)]
        public string Name { get; set; }
        [DatatableColumn("نام خانوادگی", showInReport: true, dataType: ColumnDataType.Text, filterType: ColumnFilterType.Containing, isFilterable: true)]
        public string Family { get; set; }

        [DatatableColumn("تاریخ ثبت نام", showInReport: true, dataType: ColumnDataType.Date, filterType: ColumnFilterType.Interval, isFilterable: true)]
        public DateTime Created { get; set; }

        public override IEnumerable<UserListVm> Map(IEnumerable<User> source)
        {
            return source.Select(x => new UserListVm
            {
                Id = x.Id,
                Name = x.Name,
                Family = x.Family,
                Created = x.Created
            }).ToList();
        }
    }
}
