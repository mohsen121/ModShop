using ModShop.Application.BaseEntities.Commands.CreateBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Colors.Commands.CreateColor
{
    public class CreateColorCommand : ICreateBaseEntityCommand<Color>
    {
        public Color Entity { get; set; }
    }

    public class CreateColorCommandHandler : CreateBaseEntityCommandHandler<Color, CreateColorCommand>
    {
        public CreateColorCommandHandler(IAppDb appDb) : base(appDb)
        {
        }

        public override Task<Color> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            request.Entity.HexCode = request.Entity.HexCode.Replace("#", "");
            return base.Handle(request, cancellationToken);
        }
    }
}
