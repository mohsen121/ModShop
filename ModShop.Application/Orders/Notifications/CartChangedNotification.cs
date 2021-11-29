using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Orders.Notifications
{
    public class CartChangedNotification : INotification
    {
        public Guid OrderId { get; set; }
    }

    public class CartChangedNotificationHandler : INotificationHandler<CartChangedNotification>
    {
        private IAppDb _appDb;

        public CartChangedNotificationHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task Handle(CartChangedNotification notification, CancellationToken cancellationToken)
        {
            var order = await _appDb.Orders
                .Include(x => x.Items)
                .Where(x => x.Id == notification.OrderId)
                .FirstOrDefaultAsync(cancellationToken);
            if (order.Items != null)
            {
                order.TotalPrice = order.Items.Sum(x => (x.Price) * x.Quantity);
                order.Discount = order.Items.Sum(x => x.Discount);
                order.Tax = Math.Round((order.TotalPrice - order.Discount) * (9.0 / 100.0));

                _appDb.Orders.Update(order);
                await _appDb.SaveChangesAsync(cancellationToken);
            }

        }
    }
}
