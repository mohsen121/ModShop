using ModShop.Application.BaseEntities.Commands.DeleteBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Brands.Commands.DeleteBrand
{
    public class DeleteBrandCommand : IDeleteBaseEntityCommand<Brand>
    {
        public Brand Entity { get; set; }
    }

    public class DeleteBrandCommandHandler : DeleteBaseEntityCommandHandler<Brand, DeleteBrandCommand>
    {
        public DeleteBrandCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
