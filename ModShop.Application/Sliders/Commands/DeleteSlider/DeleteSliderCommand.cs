using ModShop.Application.BaseEntities.Commands.DeleteBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sliders.Commands.DeleteSlider
{
    public class DeleteSliderCommand : IDeleteBaseEntityCommand<Slider>
    {
        public Slider Entity { get; set; }
    }

    public class DeleteSliderCommandHandler : DeleteBaseEntityCommandHandler<Slider, DeleteSliderCommand>
    {
        public DeleteSliderCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
