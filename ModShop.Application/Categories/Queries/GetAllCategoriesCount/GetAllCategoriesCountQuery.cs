using ModShop.Application.BaseEntities.Queries.GetAllBaseEntitiesCount;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Categories.Queries.GetAllCategoriesCount
{
    public class GetAllCategoriesCountQuery : IGetAllBaseEntitiesCountQuery<Category>
    {
    }

    public class GetAllCategoriesCountQueryHandler : GetAllBaseEntitiesCountQueryHandler<Category, GetAllCategoriesCountQuery>
    {
        public GetAllCategoriesCountQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
