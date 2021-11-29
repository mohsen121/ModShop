using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Orders.Commands.PayOrder
{
    public class PayOrderCommand : IRequest<Payment>
    {
    }

    public class PayOrderCommandHandler : IRequestHandler<PayOrderCommand, Payment>
    {
        private IAppDb _appDb;
        private ICurrentUserService _currentUserService;

        public PayOrderCommandHandler(IAppDb appDb, ICurrentUserService currentUserService)
        {
            _appDb = appDb;
            _currentUserService = currentUserService;
        }
        public async Task<Payment> Handle(PayOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _appDb.Orders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserId == _currentUserService.UserId && !x.IsPaid);


            if (order != null)
            {
                order.IsPaid = true;
                order.PayTime = DateTime.Now;
                foreach (var item in order.Items)
                {
                    var product = await _appDb.Products.Where(x => x.Id == item.ProductId).FirstOrDefaultAsync(cancellationToken);
                    product.Quantity -= item.Quantity;
                    await _appDb.Update(product, cancellationToken);
                }
                await _appDb.Update(order, cancellationToken);

                var payment = new Payment
                {
                    Amount = order.TotalPrice - order.Discount + order.Tax,
                    OrderId = order.Id,
                    Token = Guid.NewGuid().ToString(),
                    Status = PaymentStatus.Success,
                    PayTime = null,
                    RRN = new Random().Next(100000, 999999).ToString(),
                    Gatway = PaymentGatway.Mellat,
                    UserId = order.UserId,
                    TraceNumber = new Random().Next(100000, 999999).ToString(),
                };

                await _appDb.Add(payment, cancellationToken);
                await _appDb.SaveChangesAsync(cancellationToken);

                return payment;
            }

            return null;
        }
    }
}
