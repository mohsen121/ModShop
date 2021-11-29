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

namespace ModShop.Application.Colors.Commands.EditColor
{
    public class EditColorValidator : AbstractValidator<EditColorCommand>
    {
        private IAppDb _appDb;

        public EditColorValidator(IAppDb appDb)
        {
            _appDb = appDb;
            RuleFor(x => x.Entity.Title)
                .NotNull().NotEmpty().WithMessage("عنوان نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول عنوان نباید کمتر از 3 باشد")
                .MustAsync((c1, t, c, t2) => BeUniqueTitle(c1.Entity.Id, t, t2)).WithMessage("رکوردی با این عنوان وجود دارد")
                ;

            RuleFor(x => x.Entity.HexCode)
                .NotNull().NotEmpty().WithMessage("کد رنگ نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول عنوان نباید کمتر از 3 باشد")
                .MaximumLength(6).WithMessage("طول عنوان نباید بیشتر از 6 باشد")
                .MustAsync((c1, code, c, t2) => BeUniqueHexCode(c1.Entity.Id, code, t2)).WithMessage("رنگ با این کد وجود دارد")
                ;
        }

        private Task<bool> BeUniqueHexCode(Guid id, string code, CancellationToken token)
        {
            return _appDb.Colors.AllAsync(x => x.Id != id && x.HexCode != code, token);
        }

        private Task<bool> BeUniqueTitle(Guid id, string title, CancellationToken token)
        {
            return _appDb.Colors.AllAsync(x => x.Id != id && x.Title != title, token);
        }
    }
}
