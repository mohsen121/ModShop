using MediatR;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Products.Queries.GetMaxAndMinPrice
{
    public class GetMaxAndMinPriceQuery : IRequest<Tuple<double, double>>
    {
    }

    public class GetMaxAndMinPriceQueryHandler : IRequestHandler<GetMaxAndMinPriceQuery, Tuple<double, double>>
    {
        private IAppDb _appDb;

        public GetMaxAndMinPriceQueryHandler(IAppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task<Tuple<double, double>> Handle(GetMaxAndMinPriceQuery request, CancellationToken cancellationToken)
        {
            var min = await _appDb.Products.MinAsync(x => x.Price);
            var max = await _appDb.Products.MaxAsync(x => x.Price);

            return new Tuple<double, double>(min, max);
        }
    }
}
