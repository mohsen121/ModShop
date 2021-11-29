using MediatR;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.BaseEntities.Commands.EditBaseEntity
{
    public interface IEditBaseEntityCommand<TEntity> : IRequest<TEntity>
        where TEntity : BaseEntity
    {
        public TEntity Entity { get; set; }
    }

    public class EditBaseEntityCommandHandler<TEntity, TCommand> : IRequestHandler<TCommand, TEntity>
        where TEntity : BaseEntity, new()
        where TCommand : class, IEditBaseEntityCommand<TEntity>, new()
    {
        private IAppDb _appDb;

        public EditBaseEntityCommandHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public virtual async Task<TEntity> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var entity = await _appDb.Update(request.Entity, cancellationToken);
            await _appDb.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
