using ModShop.Application.BaseEntities.Commands.CreateBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : ICreateBaseEntityCommand<Category>
    {
        public Category Entity { get; set; }
    }

    public class CreateCategoryCommandHandler : CreateBaseEntityCommandHandler<Category, CreateCategoryCommand>
    {
        public CreateCategoryCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
