using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Application.Orders.Notifications;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Orders.Commands.AddToCart
{
    public class AddToCartCommand : IRequest
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }

    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
    {
        private IAppDb _appDb;
        private ICurrentUserService _currentUserService;
        private IMediator _mediator;

        public AddToCartCommandHandler(IAppDb appDb, ICurrentUserService currentUserService, IMediator mediator)
        {
            _appDb = appDb;
            _currentUserService = currentUserService;
            _mediator = mediator;
        }
        public async Task<Unit> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var order = await _appDb.Orders
                .Include(x => x.Items)
                .Where(x => x.UserId == _currentUserService.UserId && !x.IsPaid).FirstOrDefaultAsync();

            var product = await _appDb.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId, cancellationToken);

            if (product.Quantity < request.Count && product.BaseProductId != null)
            {
                product = await _appDb.Products.Where(x => x.BaseProductId == product.BaseProductId && x.Quantity >= request.Count).FirstOrDefaultAsync();
            }
            if (product.BaseProductId == null)
            {
                product = await _appDb.Products
                    .Where(x => x.BaseProductId == product.Id && x.Quantity >= request.Count).FirstOrDefaultAsync();
            }

            if (order != null)
            {
                var item = order.Items.FirstOrDefault(x => x.ProductId == request.ProductId);
                if (item != null)
                {
                    item.Quantity += request.Count;
                    _appDb.OrderItems.Update(item);
                    await _appDb.SaveChangesAsync(cancellationToken);
                }
                else
                {



                    item = new Domain.Entities.OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = request.Count,
                        OrderId = order.Id,
                        Price = Math.Round(product.Price),
                        Discount = product.Discount
                    };

                    await _appDb.OrderItems.AddAsync(item, cancellationToken);
                    await _appDb.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {
                order = new Order
                {
                    UserId = _currentUserService.UserId,
                    IsPaid = false,
                    Items = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductId = product.Id,
                            Price = Math.Round(product.Price),
                            Quantity = request.Count,
                            Discount = product.Discount
                        }
                    }
                };
                await _appDb.Orders.AddAsync(order, cancellationToken);
                await _appDb.SaveChangesAsync(cancellationToken);
            }

            var notif = new CartChangedNotification
            {
                OrderId = order.Id
            };
            await _mediator.Publish(notif, cancellationToken);

            return Unit.Value;
        }
    }
}
