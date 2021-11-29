using MediatR;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.BaseEntities.Commands.DeleteBaseEntity
{
    public interface IDeleteBaseEntityCommand<TEntity> : IRequest
        where TEntity : BaseEntity
    {
        public TEntity Entity { get; set; }
    }

    public class DeleteBaseEntityCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand>
        where TEntity : BaseEntity, new()
        where TCommand : class, IDeleteBaseEntityCommand<TEntity>, new()
    {
        private readonly IAppDb _appDb;

        public DeleteBaseEntityCommandHandler(IAppDb appDb)
        {
            this._appDb = appDb;
        }
        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            await _appDb.Delete(request.Entity, cancellationToken);
            await _appDb.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
