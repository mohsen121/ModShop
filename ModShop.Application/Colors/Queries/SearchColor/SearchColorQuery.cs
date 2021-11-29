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

namespace ModShop.Application.Colors.Queries.SearchColor
{
    public class SearchColorQuery : IRequest<List<Color>>
    {
        public string SearchQuery { get; set; }
    }

    public class SearchColorQueryHandler : IRequestHandler<SearchColorQuery, List<Color>>
    {
        private readonly IAppDb _appDb;

        public SearchColorQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }

        public Task<List<Color>> Handle(SearchColorQuery request, CancellationToken cancellationToken)
        {
            return _appDb.Colors.AsNoTracking()
                .Where(x => x.Title.Contains(request.SearchQuery) || x.HexCode.Contains(request.SearchQuery))
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
