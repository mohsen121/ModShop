using ModShop.Application.BaseEntities.Queries.GetAllBaseEntitiesCount;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Colors.Queries.GetAllColorsCount
{
    public class GetAllColorsCountQuery : IGetAllBaseEntitiesCountQuery<Color>
    {
    }

    public class GetAllColorsCountQueryHandler : GetAllBaseEntitiesCountQueryHandler<Color, GetAllColorsCountQuery>
    {
        public GetAllColorsCountQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
