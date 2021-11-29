using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Users.Queries.GetUsersPaged
{
    public class GetUsersPagedQuery : IRequest<Tuple<List<User>, int>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDir { get; set; }
        public string Search { get; set; }
        public string[] Includes { get; set; }
        public Dictionary<string, string[]> Filters { get; set; }
    }

    public class GetUsersPagedQueryHandler : IRequestHandler<GetUsersPagedQuery, Tuple<List<User>, int>>
    {
        private IAppDb _appDb;

        public GetUsersPagedQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<Tuple<List<User>, int>> Handle(GetUsersPagedQuery request, CancellationToken cancellationToken)
        {
            var query = _appDb.Set<User>()
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
                    query = query.OrderBy(request.OrderColumn, "asc");
                else
                {
                    query = query.OrderByDescending(x => x.Created);
                }
            }
            else
            {
                query = query.OrderBy(request.OrderColumn, request.OrderDir);
            }

            if (request.Filters != null && request.Filters.Any())
            {
                foreach (var filter in request.Filters)
                    query = query.Where(filter.Key);
            }

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search) || x.Email.Contains(request.Search));
            }


            return (request.Length == int.MaxValue ? new Tuple<List<User>, int>(await query.ToListAsync(cancellationToken), await query.CountAsync(cancellationToken)) : new Tuple<List<User>, int>(await query
                //.Skip((request.Start - 1) * request.Length)
                .Skip(request.Start)
                .Take(request.Length)
                .ToListAsync(cancellationToken), await query.CountAsync(cancellationToken)));
        }
    }
}
