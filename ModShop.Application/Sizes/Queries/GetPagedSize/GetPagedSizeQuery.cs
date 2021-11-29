using ModShop.Application.BaseEntities.Queries.GetPagedBaseEntities;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sizes.Queries.GetPagedSize
{
    public class GetPagedSizeQuery : IGetPagedBaseEntitiesQuery<Size>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDir { get; set; }
        public string Search { get; set; }
        public string SearchExpression { get; set; }
        public string[] Includes { get; set; }
        public Dictionary<string, object[]> Filters { get; set; }
        public Expression<Func<Size, bool>> SearchFunc { get; set; }
    }

    public class GetPagedSizeQueryHandler : GetPagedBaseEntitiesQueryHandler<Size, GetPagedSizeQuery>
    {
        public GetPagedSizeQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
