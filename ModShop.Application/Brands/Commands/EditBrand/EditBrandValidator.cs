using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Brands.Commands.EditBrand
{
    public class EditBrandValidator : AbstractValidator<EditBrandCommand>
    {
        private IAppDb _appDb;

        public EditBrandValidator(IAppDb appDb)
        {
            _appDb = appDb;
            RuleFor(x => x.Entity.Title)
                .NotNull().NotEmpty().WithMessage("عنوان نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول عنوان نباید کمتر از 3 باشد")
                .MustAsync((c1, t, c, t2) => BeUniqueTitle(c1.Entity.Id, t, t2)).WithMessage("رکوردی با این عنوان وجود دارد")
                ;
        }

        private Task<bool> BeUniqueTitle(Guid id, string title, CancellationToken token)
        {
            return _appDb.Brands.AllAsync(x => x.Id != id && x.Title != title, token);
        }
    }
}
