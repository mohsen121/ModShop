using ModShop.Application.BaseEntities.Queries.GetBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.OrderItems.Queries.GetOrderItem
{
    public class GetOrderItemQuery : IGetBaseEntityQuery<OrderItem, Guid>
    {
        public Guid Id { get; set; }
        public string[] Includes { get; set; }
    }

    public class GetOrderItemQueryHandler : GetBaseEntityQueryHandler<OrderItem, Guid, GetOrderItemQuery>
    {
        public GetOrderItemQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
