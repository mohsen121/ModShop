using ModShop.Application.BaseEntities.Queries.GetAllBaseEntitiesCount;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sizes.Queries.GetAllSizesCount
{
    public class GetAllSizesCountQuery : IGetAllBaseEntitiesCountQuery<Size>
    {
    }
    public class GetAllSizesCountQueryHandler : GetAllBaseEntitiesCountQueryHandler<Size, GetAllSizesCountQuery>
    {
        public GetAllSizesCountQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
