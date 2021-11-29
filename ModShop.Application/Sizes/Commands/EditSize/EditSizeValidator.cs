using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Sizes.Commands.EditSize
{
    public class EditSizeValidator : AbstractValidator<EditSizeCommand>
    {
        private IAppDb _appDb;

        public EditSizeValidator(IAppDb appDb)
        {
            _appDb = appDb;
            RuleFor(x => x.Entity.Title)
                .NotNull().NotEmpty().WithMessage("عنوان نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول عنوان نباید کمتر از 3 باشد")
                .MustAsync((en, title, context, token) => BeUniqueTitle(en.Entity.Id, title, token)).WithMessage("رکوردی با این عنوان وجود دارد")
                ;
        }

        private Task<bool> BeUniqueTitle(Guid id, string title, CancellationToken token)
        {
            return _appDb.Sizes.AllAsync(x => x.Id != id && x.Title != title, token);
        }
    }
}
