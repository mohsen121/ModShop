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
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Linq.Expressions;

namespace ModShop.Application.BaseEntities.Queries.GetPagedBaseEntities
{
    public interface IGetPagedBaseEntitiesQuery<TEntity> : IRequest<Tuple<List<TEntity>, int>>
        where TEntity : BaseEntity, new()
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDir { get; set; }
        public string Search { get; set; }
        public string SearchExpression { get; set; }
        public string[] Includes { get; set; }
        public Dictionary<string, object[]> Filters { get; set; }

        public Expression<Func<TEntity, bool>> SearchFunc { get; set; }
    }

    public abstract class GetPagedBaseEntitiesQueryHandler<TEntity, TQuery> : IRequestHandler<TQuery, Tuple<List<TEntity>, int>>
        where TEntity : BaseEntity, new()
        where TQuery : IGetPagedBaseEntitiesQuery<TEntity>
    {
        private readonly IAppDb _appDb;

        public GetPagedBaseEntitiesQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public virtual async Task<Tuple<List<TEntity>, int>> Handle(TQuery request, CancellationToken cancellationToken)
        {
            var query = _appDb.Set<TEntity>()
                .AsQueryable();

            if (request.Includes != null)
            {
                foreach (var i in request.Includes)
                    query = query.Include(i);
            }

            query = query.AsNoTracking();



            if (string.IsNullOrEmpty(request.OrderDir))
            {
                if (!string.IsNullOrEmpty(request.OrderColumn))
                    query = query.OrderBy($"{request.OrderColumn} desc");
                else
                {
                    query = query.OrderByDescending(x => x.Created);
                }
            }
            else
            {
                query = query.OrderBy($"{request.OrderColumn} {request.OrderDir}");
            }

            if (request.Filters != null && request.Filters.Any())
            {
                //var filter = string.Join("||", request.Filters.Select(x => x.Key));
                //query = query.Where(filter, request.Filters.Select(x => x.Value).First());
                foreach (var f in request.Filters)
                {
                    query = query.Where(f.Key);
                }
            }
            //request.Start = request.Start == 0 ? request.Start + 1 : request.Start;

            if (!string.IsNullOrEmpty(request.Search) && !string.IsNullOrEmpty(request.SearchExpression))
            {
                //var result = await query.ToListAsync();
                //var searchList = result.Where(x => request.SearchFunc(x));
                //return request.Length == int.MaxValue ? new Tuple<List<TEntity>, int>(searchList.ToList(), searchList.Count()) : new Tuple<List<TEntity>, int>(searchList
                //    //.Skip((request.Start - 1) * request.Length)
                //    .Skip(request.Start)
                //.Take(request.Length)
                //.ToList(), searchList.Count());

                query = query.Where(request.SearchExpression, request.Search);
            }

            return (request.Length == int.MaxValue ? new Tuple<List<TEntity>, int>(await query.ToListAsync(cancellationToken), await query.CountAsync(cancellationToken)) : new Tuple<List<TEntity>, int>(await query
                //.Skip((request.Start - 1) * request.Length)
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync(cancellationToken), await query.CountAsync(cancellationToken)));
        }
    }
}
