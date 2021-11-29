using ModShop.Application.BaseEntities.Queries.GetBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Colors.Queries.GetColor
{
    public class GetColorQuery : IGetBaseEntityQuery<Color, Guid>
    {
        public Guid Id { get; set; }
        public string[] Includes { get; set; }
    }

    public class GetColorQueryHandler : GetBaseEntityQueryHandler<Color, Guid, GetColorQuery>
    {
        public GetColorQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
