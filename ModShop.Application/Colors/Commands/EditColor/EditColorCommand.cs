using ModShop.Application.BaseEntities.Commands.EditBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Colors.Commands.EditColor
{
    public class EditColorCommand : IEditBaseEntityCommand<Color>
    {
        public Color Entity { get; set; }
    }

    public class EditColorCommandHandler : EditBaseEntityCommandHandler<Color, EditColorCommand>
    {
        public EditColorCommandHandler(IAppDb appDb) : base(appDb)
        {
        }

        public override Task<Color> Handle(EditColorCommand request, CancellationToken cancellationToken)
        {
            request.Entity.HexCode = request.Entity.HexCode.Replace("#", "");
            return base.Handle(request, cancellationToken);
        }
    }
}
