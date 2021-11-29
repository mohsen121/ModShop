using ModShop.Application.BaseEntities.Queries.GetPagedBaseEntities;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Colors.Queries.GetPagedColor
{
    public class GetPagedColorsQuery : IGetPagedBaseEntitiesQuery<Color>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDir { get; set; }
        public string Search { get; set; }
        public string SearchExpression { get; set; }
        public string[] Includes { get; set; }
        public Dictionary<string, object[]> Filters { get; set; }
        public Expression<Func<Color, bool>> SearchFunc { get; set; }
    }

    public class GetPagedColorsQueryHandler : GetPagedBaseEntitiesQueryHandler<Color, GetPagedColorsQuery>
    {
        public GetPagedColorsQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
