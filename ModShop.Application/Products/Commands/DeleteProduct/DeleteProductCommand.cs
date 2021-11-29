using ModShop.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private IAppDb _appDb;

        public DeleteProductCommandHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _appDb.Products.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (entity != null)
            {
                _appDb.Products.Remove(entity);
                await _appDb.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;
        }
    }
}
