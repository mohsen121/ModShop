using ModShop.Application.BaseEntities.Queries.GetBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Categories.Queries.GetCategory
{
    public class GetCategoryQuery : IGetBaseEntityQuery<Category, Guid>
    {
        public Guid Id { get; set; }
        public string[] Includes { get; set; }
    }

    public class GetCategoryQueryHandler : GetBaseEntityQueryHandler<Category, Guid, GetCategoryQuery>
    {
        public GetCategoryQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
