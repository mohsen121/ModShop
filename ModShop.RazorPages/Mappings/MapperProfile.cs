using AutoMapper;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductPageListVm>();
        }
    }
}
