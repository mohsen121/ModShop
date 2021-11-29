using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ModShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModShop.Application.Products.Commands.EditProduct
{
    public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
    {
        private IAppDb _appDb;

        public EditProductCommandValidator(IAppDb appDb)
        {
            _appDb = appDb;

            RuleFor(v => v.Entity.Title)
                .NotEmpty().WithMessage("عنوان ضروری است.")
                .MaximumLength(100).WithMessage("طول عنوان نباید بیش از 100 کاراکتر باشد.");
            //.MustAsync(BeUniqueTitle).WithMessage("محصول با این عنوان وجود دارد.");

            RuleFor(x => x.Entity.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("تعداد موجود محصول باید برابر یا بیش از 0 باشد.");

            RuleFor(x => x.Entity.Price)
                .GreaterThanOrEqualTo(0).WithMessage("قیمت محصول باید برابر یا بیش از 0 باشد.");

            RuleFor(x => x.Entity.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("تخفبف محصول باید برابر یا بیش از 0 باشد.");

            RuleFor(x => x.Entity.CategoryId)
                .NotEmpty().WithMessage("دسته بندی ضروری است.")
                .MustAsync(BeExistCategory).WithMessage("این دسته بندی مجاز نمی باشد");

            RuleFor(x => x.Entity.BrandId)
                .NotEmpty().WithMessage("برند ضروری است.")
                .MustAsync(BeExistBrand).WithMessage("این برند مجاز نمی باشد");

            RuleFor(x => x.Entity.ColorId)
                .NotEmpty().When(x => x.Entity.BaseProductId.HasValue).WithMessage("وقتی محصول پایه را انتخاب کردید باید رنگ هم انتخاب کنید")
                .MustAsync(BeExistColor).WithMessage("این رنگ مجاز نمی باشد.");

            RuleFor(x => x.Entity.SizeId)
               .NotEmpty().When(x => x.Entity.BaseProductId.HasValue).WithMessage("وقتی محصول پایه را انتخاب کردید باید سایز هم انتخاب کنید")
               .MustAsync(BeExistSize).WithMessage("این سایز مجاز نمی باشد.");


        }

        private async Task<bool> BeExistSize(Guid? arg, CancellationToken cancellationToken)
        {
            if (arg.HasValue)
                return await _appDb.Sizes
                    .AnyAsync(x => x.Id == arg.Value, cancellationToken);

            return true;
        }

        private async Task<bool> BeExistColor(Guid? arg, CancellationToken cancellationToken)
        {
            if (arg.HasValue)
                return await _appDb.Colors
                    .AnyAsync(x => x.Id == arg.Value, cancellationToken);

            return true;
        }

        public async Task<bool> BeExistCategory(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _appDb.Categories
                .AnyAsync(x => x.Id == categoryId, cancellationToken);
        }

        public async Task<bool> BeExistBrand(Guid brandId, CancellationToken cancellationToken)
        {
            return await _appDb.Brands
                .AnyAsync(x => x.Id == brandId, cancellationToken);
        }



        //public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        //{
        //    return await _appDb.Products
        //        .AllAsync(l => l.Title != title && l.Id != , cancellationToken);
        //}
    }
}
