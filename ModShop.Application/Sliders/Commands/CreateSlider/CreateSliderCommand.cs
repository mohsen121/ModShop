using ModShop.Application.BaseEntities.Commands.CreateBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sliders.Commands.CreateSlider
{
    public class CreateSliderCommand : ICreateBaseEntityCommand<Slider>
    {
        public Slider Entity { get; set; }
    }

    public class CreateSliderCommandHander : CreateBaseEntityCommandHandler<Slider, CreateSliderCommand>
    {
        public CreateSliderCommandHander(IAppDb appDb) : base(appDb)
        {
        }
    }
}
