using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Payments.Queries.GetPagedPayments;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Payments
{
    public class PaymentListModel : BaseListPage<Payment, PaymentListVm, GetPagedPaymentsQuery>
    {
        public PaymentListModel() : base("created", "desc")
        {

        }
        [BindProperty]
        public override DatatableDto Dto { get; set; }
    }
}
