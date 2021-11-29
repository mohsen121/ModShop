using ModShop.Application.BaseEntities.Commands.EditBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Brands.Commands.EditBrand
{
    public class EditBrandCommand : IEditBaseEntityCommand<Brand>
    {
        public Brand Entity { get; set; }
    }

    public class EditBrandCommandHandler : EditBaseEntityCommandHandler<Brand, EditBrandCommand>
    {
        public EditBrandCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
