using ModShop.Application.BaseEntities.Queries.GetAllBaseEntitiesCount;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Brands.Queries.GetAllBrandsCount
{
    public class GetAllBrandsCountQuery : IGetAllBaseEntitiesCountQuery<Brand>
    {
    }

    public class GetAllBrandsCountQueryHandler : GetAllBaseEntitiesCountQueryHandler<Brand, GetAllBrandsCountQuery>
    {
        public GetAllBrandsCountQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
