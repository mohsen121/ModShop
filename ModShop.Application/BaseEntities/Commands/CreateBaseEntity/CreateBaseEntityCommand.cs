using MediatR;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.BaseEntities.Commands.CreateBaseEntity
{
    public interface ICreateBaseEntityCommand<TEntity> : IRequest<TEntity>
        where TEntity : BaseEntity, new()
    {
        public TEntity Entity { get; set; }
    }

    public abstract class CreateBaseEntityCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, TEntity>
        where TEntity : BaseEntity, new()
        where TCommand : class, ICreateBaseEntityCommand<TEntity>, new()
    {
        private IAppDb _appDb;

        public CreateBaseEntityCommandHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }

        public virtual async Task<TEntity> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await _appDb.Set<TEntity>().AddAsync(request.Entity, cancellationToken);
            await _appDb.SaveChangesAsync(cancellationToken);

            return entity.Entity;
        }
    }
}
