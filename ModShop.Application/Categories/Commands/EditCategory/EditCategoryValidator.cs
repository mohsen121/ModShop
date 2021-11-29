using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Categories.Commands.EditCategory
{
    public class EditCategoryValidator : AbstractValidator<EditCategoryCommand>
    {
        private IAppDb _appDb;

        public EditCategoryValidator(IAppDb appDb)
        {
            _appDb = appDb;
            RuleFor(x => x.Entity.Title)
                .NotNull().NotEmpty().WithMessage("عنوان نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول عنوان نباید کمتر از 3 باشد")
                .MustAsync((c1, t, token) => BeUniqueTitle(c1.Entity.Id, t, token)).WithMessage("رکوردی با این عنوان وجود دارد")
                ;

            RuleFor(x => x.Entity.ParentId)
                .NotEqual(x => x.Entity.Id).WithMessage("دسته بندی والد نمیتواند با خود دسته بندی یکی باشد")
                .MustAsync((cat, parentId, token) => BeExistParent(cat.Entity.Id, parentId, token)).WithMessage("دسته بندی والد وجود ندارد")
                ;
        }

        private Task<bool> BeExistParent(Guid id, Guid? parentId, CancellationToken token)
        {
            if (parentId.HasValue)
            {
                return _appDb.Categories.AnyAsync(x => x.Id == parentId.Value, token);
            }

            return Task.FromResult(true);
        }

        private Task<bool> BeUniqueTitle(Guid id, string title, CancellationToken token)
        {
            return _appDb.Categories.AllAsync(x => x.Id != id && x.Title != title, token);
        }
    }
}
