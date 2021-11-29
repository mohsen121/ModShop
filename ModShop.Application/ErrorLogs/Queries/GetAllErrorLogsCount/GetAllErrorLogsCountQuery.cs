using ModShop.Application.BaseEntities.Queries.GetAllBaseEntitiesCount;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.ErrorLogs.Queries.GetAllErrorLogsCount
{
    public class GetAllErrorLogsCountQuery : IGetAllBaseEntitiesCountQuery<ErrorLog>
    {
    }

    public class GetAllErrorLogsCountQueryHandler : GetAllBaseEntitiesCountQueryHandler<ErrorLog, GetAllErrorLogsCountQuery>
    {
        public GetAllErrorLogsCountQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
