using ModShop.Application.BaseEntities.Queries.GetBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sizes.Queries.GetSize
{
    public class GetSizeQuery : IGetBaseEntityQuery<Size, Guid>
    {
        public Guid Id { get; set; }
        public string[] Includes { get; set; }
    }

    public class GetSizeQueryHandler : GetBaseEntityQueryHandler<Size, Guid, GetSizeQuery>
    {
        public GetSizeQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
