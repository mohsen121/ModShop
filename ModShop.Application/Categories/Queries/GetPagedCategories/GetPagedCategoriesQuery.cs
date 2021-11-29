using ModShop.Application.BaseEntities.Queries.GetPagedBaseEntities;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Categories.Queries.GetPagedCategories
{
    public class GetPagedCategoriesQuery : IGetPagedBaseEntitiesQuery<Category>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDir { get; set; }
        public string Search { get; set; }
        public string SearchExpression { get; set; }
        public string[] Includes { get; set; }
        public Dictionary<string, object[]> Filters { get; set; }
        public Expression<Func<Category, bool>> SearchFunc { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public class GetPagedCategoriesQueryHandler : GetPagedBaseEntitiesQueryHandler<Category, GetPagedCategoriesQuery>
    {
        public GetPagedCategoriesQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
