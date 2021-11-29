using ModShop.Application.BaseEntities.Commands.EditBaseEntity;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Application.Categories.Commands.EditCategory
{
    public class EditCategoryCommand : IEditBaseEntityCommand<Category>
    {
        public Category Entity { get; set; }
    }

    public class EditCategoryCommandHandler : EditBaseEntityCommandHandler<Category, EditCategoryCommand>
    {
        public EditCategoryCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
