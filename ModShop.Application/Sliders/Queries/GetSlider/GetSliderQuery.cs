using ModShop.Application.BaseEntities.Queries.GetBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sliders.Queries.GetSlider
{
    public class GetSliderQuery : IGetBaseEntityQuery<Slider, Guid>
    {
        public Guid Id { get; set; }
        public string[] Includes { get; set; }
    }

    public class GetSliderQueryHandler : GetBaseEntityQueryHandler<Slider, Guid, GetSliderQuery>
    {
        public GetSliderQueryHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
