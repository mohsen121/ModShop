using ModShop.Application.BaseEntities.Commands.DeleteBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Sizes.Commands.DeleteSize
{
    public class DeleteSizeCommand : IDeleteBaseEntityCommand<Size>
    {
        public Size Entity { get; set; }
    }

    public class DeleteSizeCommandHandler : DeleteBaseEntityCommandHandler<Size, DeleteSizeCommand>
    {
        public DeleteSizeCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
