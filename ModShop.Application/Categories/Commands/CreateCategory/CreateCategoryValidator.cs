using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommand>
    {
        private IAppDb _appDb;

        public CreateCategoryValidator(IAppDb appDb)
        {
            _appDb = appDb;
            RuleFor(x => x.Entity.Title)
                .NotNull().NotEmpty().WithMessage("عنوان نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول عنوان نباید کمتر از 3 باشد")
                .MustAsync(BeUniqueTitle).WithMessage("رکوردی با این عنوان وجود دارد")
                ;

            RuleFor(x => x.Entity.ParentId)
                .MustAsync(BeExistsParentId).WithMessage("دسته بندی والد وجود ندارد")
                ;
        }

        private async Task<bool> BeExistsParentId(Guid? arg1, CancellationToken arg2)
        {
            if (arg1 == null)
                return true;

            return await _appDb.Categories.AnyAsync(x => x.Id == arg1.Value, arg2);
        }

        private Task<bool> BeUniqueTitle(string title, CancellationToken arg2)
        {
            return _appDb.Categories.AllAsync(l => l.Title != title, arg2);
        }
    }
}
