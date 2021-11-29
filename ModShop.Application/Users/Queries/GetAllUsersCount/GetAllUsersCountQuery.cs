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

namespace ModShop.Application.Users.Queries.GetAllUsersCount
{
    public class GetAllUsersCountQuery : IRequest<int>
    {
    }

    public class GetAllUsersCountQueryHandler : IRequestHandler<GetAllUsersCountQuery, int>
    {
        private IAppDb _appDb;

        public GetAllUsersCountQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public Task<int> Handle(GetAllUsersCountQuery request, CancellationToken cancellationToken)
        {
            return _appDb.Set<User>().CountAsync(cancellationToken);
        }
    }
}
