using ModShop.Application.BaseEntities.Queries.GetBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Orders.Queries.GetOrder
{
    public class GetOrderQuery : IGetBaseEntityQuery<Order, Guid>
    {
        public Guid Id { get; set; }
        public string[] Includes { get; set; }
    }

    public class GetOrderQueryHandler : GetBaseEntityQueryHandler<Order, Guid, GetOrderQuery>
    {
        public GetOrderQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
