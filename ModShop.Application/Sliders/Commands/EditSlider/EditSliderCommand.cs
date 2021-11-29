using ModShop.Application.BaseEntities.Commands.EditBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sliders.Commands.EditSlider
{
    public class EditSliderCommand : IEditBaseEntityCommand<Slider>
    {
        public Slider Entity { get; set; }
    }

    public class EditSliderCommandHandler : EditBaseEntityCommandHandler<Slider, EditSliderCommand>
    {
        public EditSliderCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
