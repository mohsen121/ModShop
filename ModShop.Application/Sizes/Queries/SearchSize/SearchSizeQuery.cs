using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Sizes.Queries.SearchSize
{
    public class SearchSizeQuery : IRequest<List<Size>>
    {
        public string SearchQuery { get; set; }
    }

    public class SearchSizeQueryHandler : IRequestHandler<SearchSizeQuery, List<Size>>
    {
        private IAppDb _appDb;

        public SearchSizeQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public Task<List<Size>> Handle(SearchSizeQuery request, CancellationToken cancellationToken)
        {
            return _appDb.Sizes.AsNoTracking()
                .Where(x => x.Title.Contains(request.SearchQuery) || x.Code.Contains(request.SearchQuery))
                .ToListAsync();
        }
    }
}
