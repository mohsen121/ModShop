using ModShop.Application.BaseEntities.Commands.DeleteBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Colors.Commands.DeleteColor
{
    public class DeleteColorCommand : IDeleteBaseEntityCommand<Color>
    {
        public Color Entity { get; set; }
    }

    public class DeleteColorCommandHandler : DeleteBaseEntityCommandHandler<Color, DeleteColorCommand>
    {
        public DeleteColorCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
