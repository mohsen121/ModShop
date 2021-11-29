using ModShop.Application.BaseEntities.Commands.EditBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sizes.Commands.EditSize
{
    public class EditSizeCommand : IEditBaseEntityCommand<Size>
    {
        public Size Entity { get; set; }
    }

    public class EditSizeCommandHandler : EditBaseEntityCommandHandler<Size, EditSizeCommand>
    {
        public EditSizeCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
