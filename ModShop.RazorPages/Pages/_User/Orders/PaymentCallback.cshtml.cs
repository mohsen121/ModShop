using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;

namespace ModShop.RazorPages.Pages._User.Orders
{
    public class PaymentCallbackModel : BaseUserPage
    {
        private IAppDb _appDb;

        public PaymentCallbackModel(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<IActionResult> OnGet(Guid id)
        {
            var payment = await _appDb.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (payment == null || payment.PayTime != null)
                return Redirect("/");

            var ct = new CancellationToken();
            payment.PayTime = DateTime.Now;
            await _appDb.Update(payment, ct);
            await _appDb.SaveChangesAsync(ct);
            return Page();
        }
    }
}
