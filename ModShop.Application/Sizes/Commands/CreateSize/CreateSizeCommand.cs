using ModShop.Application.BaseEntities.Commands.CreateBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sizes.Commands.CreateSize
{
    public class CreateSizeCommand : ICreateBaseEntityCommand<Size>
    {
        public Size Entity { get; set; }
    }

    public class CreateSizeCommandHandler : CreateBaseEntityCommandHandler<Size, CreateSizeCommand>
    {
        public CreateSizeCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
