using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Colors.Commands.CreateColor
{
    public class CreateColorValidator : AbstractValidator<CreateColorCommand>
    {
        private IAppDb _appDb;

        public CreateColorValidator(IAppDb appDb)
        {
            _appDb = appDb;
            RuleFor(x => x.Entity.Title)
                .NotNull().NotEmpty().WithMessage("عنوان نمیتواند خالی باشد")
                .MinimumLength(2).WithMessage("طول عنوان نباید کمتر از 2 باشد")
                .MustAsync(BeUniqueTitle).WithMessage("رکوردی با این عنوان وجود دارد")
                ;

            RuleFor(x => x.Entity.HexCode)
                .NotNull().NotEmpty().WithMessage("کد رنگ نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول عنوان نباید کمتر از 3 باشد")
                .MaximumLength(6).WithMessage("طول عنوان نباید بیشتر از 6 باشد")
                .MustAsync(BeUniqueHexCode).WithMessage("رنگ با این کد وجود دارد")
                ;
        }

        private Task<bool> BeUniqueHexCode(string arg1, CancellationToken arg2)
        {
            return _appDb.Colors.AllAsync(x => x.HexCode != arg1, arg2);
        }

        private Task<bool> BeUniqueTitle(string title, CancellationToken arg2)
        {
            return _appDb.Colors.AllAsync(l => l.Title != title, arg2);
        }
    }
}
