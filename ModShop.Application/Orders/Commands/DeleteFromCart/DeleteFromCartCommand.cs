using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Application.Orders.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Orders.Commands.DeleteFromCart
{
    public class DeleteFromCartCommand : IRequest
    {
        public Guid ProductId { get; set; }
    }

    public class DeleteFromCartCommandHandler : IRequestHandler<DeleteFromCartCommand>
    {
        private IAppDb _appDb;
        private IMediator _mediator;
        private ICurrentUserService _currentUserService;

        public DeleteFromCartCommandHandler(IAppDb appDb, IMediator mediator, ICurrentUserService currentUserService)
        {
            _appDb = appDb;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }
        public async Task<Unit> Handle(DeleteFromCartCommand request, CancellationToken cancellationToken)
        {
            var order = await _appDb.Orders
                .Include(x => x.Items)
                .Where(x => x.UserId == _currentUserService.UserId && !x.IsPaid)
                .FirstOrDefaultAsync(cancellationToken);

            if (order != null)
            {
                var item = order.Items.FirstOrDefault(x => x.ProductId == request.ProductId);
                await _appDb.Delete(item, cancellationToken);
                await _appDb.SaveChangesAsync(cancellationToken);

                var notif = new CartChangedNotification { OrderId = order.Id };
                await _mediator.Publish(notif, cancellationToken);
            }


            return Unit.Value;
        }
    }
}
