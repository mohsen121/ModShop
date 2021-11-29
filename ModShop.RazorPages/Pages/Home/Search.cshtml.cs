using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModShop.Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Colors.Queries.GetPagedColor;
using ModShop.Application.Brands.Queries.GetPagedBrands;
using ModShop.Application.Categories.Queries.GetPagedCategories;
using ModShop.Application.Sizes.Queries.GetPagedSize;
using ModShop.Application.Products.Queries.GetSearchPage;
using ModShop.Application.Products.Queries.GetMaxAndMinPrice;
using System.Threading;

namespace ModShop.RazorPages.Pages.Home
{
    public class SearchModel : BasePageModel
    {
        [BindProperty]
        public SearchFiltersVm SearchFilters { get; set; }

        [BindProperty]
        public int PageNumber { get; set; }

        [BindProperty]
        public double MaxPrice { get; set; }
        [BindProperty]
        public double MinPrice { get; set; }
        [BindProperty]
        public List<Color> Colors { get; set; }
        [BindProperty]
        public List<Brand> Brands { get; set; }
        [BindProperty]
        public List<Category> Categories { get; set; }
        [BindProperty]
        public List<Size> Sizes { get; set; }

        [BindProperty]
        public List<Product> Products { get; set; }

        [BindProperty]
        public int TotalCount { get; set; }


        public async Task<IActionResult> OnGet([FromQuery] SearchFiltersVm searchFilters, [FromQuery] int pageNumber)
        {
            SearchFilters = searchFilters;
            PageNumber = pageNumber;

            await InitilizeParams();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            await InitilizeParams();

            return Page();
        }

        private async Task InitilizeParams()
        {

            if (Colors == null || !Colors.Any())
            {
                await InitColors();
            }

            if (Brands == null || !Brands.Any())
            {
                await InitBrands();
            }

            if (Categories == null || !Categories.Any())
            {
                await InitCategories();
            }

            if (Sizes == null || !Sizes.Any())
            {
                await InitSizes();
            }

            if (double.IsNaN(MinPrice) || MinPrice == 0 || double.IsNaN(MaxPrice) || MaxPrice == 0)
            {
                await InitPrices();
            }

            if (SearchFilters == null)
            {
                SearchFilters = new SearchFiltersVm
                {
                    MinPrice = MinPrice,
                    MaxPrice = MaxPrice,
                };
            }

            if (PageNumber == 0)
                PageNumber = 1;

            var productQuery = new GetSearchPageQuery
            {
                BrandIds = SearchFilters.BrandIds,
                CategoryIds = SearchFilters.CategoryIds,
                ColorIds = SearchFilters.ColorIds,
                SizeIds = SearchFilters.SizeIds,
                MaxPrice = SearchFilters.MaxPrice,
                MinPrice = SearchFilters.MinPrice,
                Query = SearchFilters.Query,
                PageNumber = PageNumber
            };
            var result = await Mediator.Send(productQuery);

            Products = result.Item1;
            TotalCount = result.Item2;
        }

        private async Task InitPrices()
        {
            var priceQuery = new GetMaxAndMinPriceQuery();
            var result = await Mediator.Send(priceQuery);

            MinPrice = result.Item1;
            MaxPrice = result.Item2;
        }

        private async Task InitSizes()
        {
            var sizeQuery = new GetPagedSizeQuery { Start = 1, Length = int.MaxValue };
            Sizes = (await Mediator.Send(sizeQuery)).Item1;
        }

        private async Task InitCategories()
        {
            var catQuery = new GetPagedCategoriesQuery { Start = 1, Length = int.MaxValue };
            Categories = (await Mediator.Send(catQuery)).Item1;
        }

        private async Task InitBrands()
        {
            var brandQuery = new GetPagedBrandsQuery { Start = 1, Length = int.MaxValue };
            Brands = (await Mediator.Send(brandQuery)).Item1;
        }

        private async Task InitColors()
        {
            var colorQuery = new GetPagedColorsQuery { Start = 1, Length = int.MaxValue };
            Colors = (await Mediator.Send(colorQuery)).Item1;
        }
    }

    public class SearchFiltersVm
    {
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
        public Guid[] CategoryIds { get; set; }
        public Guid[] BrandIds { get; set; }
        public Guid[] ColorIds { get; set; }
        public Guid[] SizeIds { get; set; }
        public string Query { get; set; }
    }
}
