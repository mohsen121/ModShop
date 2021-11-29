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

namespace ModShop.Application.Categories.Queries.SearchCategory
{
    public class SearchCategoryQuery : IRequest<List<Category>>
    {
        public string SearchQuery { get; set; }
    }

    public class SearchCategoryQueryHandler : IRequestHandler<SearchCategoryQuery, List<Category>>
    {
        private readonly IAppDb _appDb;

        public SearchCategoryQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }

        public Task<List<Category>> Handle(SearchCategoryQuery request, CancellationToken cancellationToken)
        {
            return _appDb.Categories.AsNoTracking()
                .Where(x => x.Title.Contains(request.SearchQuery) || x.Description.Contains(request.SearchQuery))
                .ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
