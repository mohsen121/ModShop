using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Sizes.Commands.CreateSize
{
    public class CreateSizeValidator : AbstractValidator<CreateSizeCommand>
    {
        private IAppDb _appDb;

        public CreateSizeValidator(IAppDb appDb)
        {
            _appDb = appDb;
            RuleFor(x => x.Entity.Title)
                .NotNull().NotEmpty().WithMessage("عنوان نمیتواند خالی باشد")
                .MinimumLength(2).WithMessage("طول عنوان نباید کمتر از 2 باشد")
                .MustAsync(BeUniqueTitle).WithMessage("رکوردی با این عنوان وجود دارد")
                ;
        }

        private Task<bool> BeUniqueTitle(string arg1, CancellationToken arg2)
        {
            return _appDb.Sizes.AllAsync(x => x.Title != arg1, arg2);
        }
    }
}
