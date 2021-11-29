using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Application.Common.Interfaces;

namespace ModShop.Application.ErrorLogs.Queries.GetErrorLogsList
{
    public class GetErrorLogsListQuery : IRequest<ErrorLogListVm>
    {
        public int Page { get; set; } = 1;
        public int Count { get; set; } = 10;
    }

    public class GetErrorLogsListQueryHandler : IRequestHandler<GetErrorLogsListQuery, ErrorLogListVm>
    {
        private IAppDb _appDb;

        public GetErrorLogsListQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<ErrorLogListVm> Handle(GetErrorLogsListQuery request, CancellationToken cancellationToken)
        {
            var items = _appDb.ErrorLogs.OrderByDescending(x => x.Created).AsQueryable();
            if (request.Page <= 0)
            {
                request.Page = 1;
            }
            if (request.Count <= 0)
            {
                request.Count = 10;
            }

            var model = new ErrorLogListVm
            {
                Count = request.Count,
                Page = request.Page,
                Items = await items
                .Skip((request.Page - 1) * request.Count)
                .Take(request.Count)
                .Select(x => new ErrorLogVm
                {
                    ClassName = x.ClassName,
                    Created = x.Created,
                    Error = x.Error,
                    Id = x.Id,
                    MethodName = x.MethodName
                }).ToListAsync(),
                TotalCount = await items.CountAsync()
            };

            return model;
        }
    }
}
