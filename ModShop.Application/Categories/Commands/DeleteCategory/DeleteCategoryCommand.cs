using ModShop.Application.BaseEntities.Commands.DeleteBaseEntity;
using ModShop.Application.BaseEntities.Commands.EditBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IDeleteBaseEntityCommand<Category>
    {
        public Category Entity { get; set; }
    }

    public class DeleteCategoryCommandHandler : DeleteBaseEntityCommandHandler<Category, DeleteCategoryCommand>
    {
        public DeleteCategoryCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
