using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Users.Commands.CreateAdmin
{
    public class CreateAdminValidator : AbstractValidator<CreateAdminCommand>
    {
        private UserManager<User> _userManager;

        public CreateAdminValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

            RuleFor(x => x.Password)
                .NotNull().NotEmpty().WithMessage("رمز عبور نمیتواند خالی باشد")
                .MinimumLength(6).WithMessage("طول رمز عبور نباید کمتر از 6 باشد");

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmPassword)
                {
                    context.AddFailure(nameof(x.Password), "رمز عبور را به درستی تایید کنید");
                }
            });

            RuleFor(x => x.Entity.Email)
                .NotNull().NotEmpty().WithMessage("ایمیل نمیتواند خالی باشد")
                .EmailAddress().WithMessage("ایمیل معتبر نیست")
                .MustAsync(BeUnique)
                ;
            RuleFor(x => x.Entity.Name)
                .NotNull().NotEmpty().WithMessage("نام نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول نام نباید کمتر از 3 باشد");
            RuleFor(x => x.Entity.Family)
                .NotNull().NotEmpty().WithMessage("نام خانوادگی نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول نام خانوادگی نباید کمتر از 3 باشد");
            RuleFor(x => x.Entity.City)
                .NotNull().NotEmpty().WithMessage("شهر نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول شهر نباید کمتر از 3 باشد");
            RuleFor(x => x.Entity.Province)
                .NotNull().NotEmpty().WithMessage("استان نمیتواند خالی باشد")
                .MinimumLength(3).WithMessage("طول استان نباید کمتر از 3 باشد");
            RuleFor(x => x.Entity.Address)
                .NotNull().NotEmpty().WithMessage("آدرس نمیتواند خالی باشد")
                .MinimumLength(10).WithMessage("طول آدرس نباید کمتر از 10 باشد");

        }

        private async Task<bool> BeUnique(string arg1, CancellationToken arg2)
        {
            return await _userManager.FindByEmailAsync(arg1) == null;
        }

    }
}
