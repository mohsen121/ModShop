using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Brands.Commands.CreateBrand
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandCommand>
    {
        private IAppDb _appDb;

        public CreateBrandValidator(IAppDb appDb)
        {
            _appDb = appDb;
            RuleFor(x => x.Entity.Title)
                .NotNull().NotEmpty().WithMessage("عنوان نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول عنوان نباید کمتر از 3 باشد")
                .MustAsync(BeUniqueTitle).WithMessage("رکوردی با این عنوان وجود دارد")
                ;
        }

        private Task<bool> BeUniqueTitle(string arg1, CancellationToken arg2)
        {
            return _appDb.Brands.AllAsync(x => x.Title != arg1, arg2);
        }
    }
}
