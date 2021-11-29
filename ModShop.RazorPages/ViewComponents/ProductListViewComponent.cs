using MediatR;
using Microsoft.AspNetCore.Mvc;
using ModShop.Application.Products.Queries.GetPagedProducts;
using ModShop.Application.Products.Queries.GetTopSale;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._User.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.ViewComponents
{
    public class ProductListViewComponent : ViewComponent
    {
        private IMediator _mediator;
        private InputModel _inputModel;

        public ProductListViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync(InputModel input)
        {
            _inputModel = input;
            var model = new OutputModel
            {
                Products = new List<Product>(await ToProducts()),
                ListType = _inputModel.ListType
            };
            return await Task.FromResult((IViewComponentResult)View("~/ViewComponents/_ProductList.cshtml", model));
        }

        public Task<List<Product>> ToProducts() => _inputModel.ListType switch
        {
            ListType.Newest => GetNewestProducts(),
            ListType.TopSale => _mediator.Send(new GetTopSaleQuery { Count = _inputModel.Count }),
            ListType.WithDiscount => GetWithDiscount(),
            _ => GetNewestProducts()
        };

        private async Task<List<Product>> GetWithDiscount()
        {
            var filters = new Dictionary<string, object[]>
            {
                {"Discount > 0",new string[]{} },
                {"BaseProductId == null && Children.Any()",new string[]{} }
            };
            return (await _mediator.Send(new GetPagedProductsQuery { OrderColumn = "created", OrderDir = "desc", Start = 0, Length = _inputModel.Count, Filters = filters })).Item1;
        }

        private async Task<List<Product>> GetNewestProducts()
        {
            var filters = new Dictionary<string, object[]>
            {
                {"BaseProductId == null && Children.Any()",new string[]{} }
            };
            return (await _mediator.Send(new GetPagedProductsQuery { OrderColumn = "created", OrderDir = "desc", Start = 0, Length = _inputModel.Count, Filters = filters, Includes = new[] { "Children" } })).Item1;
        }

        public class OutputModel
        {
            public List<Product> Products { get; set; }
            public ListType ListType { get; set; }
        }
        public enum ListType
        {
            [Display(Name = "جدیدترین محصولات")]
            Newest,
            [Display(Name = "پرفروش ترین ها")]
            TopSale,
            [Display(Name = "تخفیف دار ها")]
            WithDiscount,

        }

        public class InputModel
        {
            public int Count { get; set; } = 4;
            public ListType ListType { get; set; } = ListType.Newest;
        }
    }
}
