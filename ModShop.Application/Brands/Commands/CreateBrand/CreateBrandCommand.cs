using ModShop.Application.BaseEntities.Commands.CreateBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : ICreateBaseEntityCommand<Brand>
    {
        public Brand Entity { get; set; }
    }

    public class CreateBrandCommandHandler : CreateBaseEntityCommandHandler<Brand, CreateBrandCommand>
    {
        public CreateBrandCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
