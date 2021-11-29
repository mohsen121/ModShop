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

namespace ModShop.Application.BaseEntities.Queries.GetBaseEntity
{
    public interface IGetBaseEntityQuery<out TEntity, TKey> : IRequest<TEntity>
        where TEntity : BaseEntity<TKey>, new()
    {
        public TKey Id { get; set; }
        public string[] Includes { get; set; }
    }

    public abstract class GetBaseEntityQueryHandler<TEntity, TKey, TQuery> : IRequestHandler<TQuery, TEntity>
        where TEntity : BaseEntity<TKey>, new()
        where TQuery : IGetBaseEntityQuery<TEntity, TKey>, new()
    {
        private readonly IAppDb _appDb;

        public GetBaseEntityQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public virtual Task<TEntity> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var set = _appDb.Set<TEntity>().AsQueryable();
            if (request.Includes != null)
            {
                foreach (var i in request.Includes)
                    set = set.Include(i);
            }

            var entity = set.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken: cancellationToken);

            return entity;
        }
    }
}
