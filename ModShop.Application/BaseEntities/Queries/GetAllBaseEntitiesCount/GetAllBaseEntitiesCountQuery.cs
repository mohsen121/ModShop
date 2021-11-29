using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.BaseEntities.Queries.GetAllBaseEntitiesCount
{
    public interface IGetAllBaseEntitiesCountQuery<TEntity> : IRequest<int>
        where TEntity : BaseEntity, new()
    {
    }

    public abstract class GetAllBaseEntitiesCountQueryHandler<TEntity, TQuery> : IRequestHandler<TQuery, int>
        where TEntity : BaseEntity, new()
        where TQuery : IGetAllBaseEntitiesCountQuery<TEntity>
    {
        private readonly IAppDb _appDb;

        public GetAllBaseEntitiesCountQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public virtual Task<int> Handle(TQuery request, CancellationToken cancellationToken)
        {
            return _appDb.Set<TEntity>().CountAsync(cancellationToken);
        }
    }
}
