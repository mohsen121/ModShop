using AutoMapper;
using ModShop.Application.Common.Interfaces;
using ModShop.Application.Common.Mappings;
using ModShop.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Application.BaseEntities.Commands.EditBaseEntity;

namespace ModShop.Application.Products.Commands.EditProduct
{
    public class EditProductCommand : IEditBaseEntityCommand<Product>
    {

        public Product Entity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, EditProductCommand>()
                ;
        }
    }

    public class EditProductCommandHandler : EditBaseEntityCommandHandler<Product, EditProductCommand>
    {
        private IAppDb _appDb;

        public EditProductCommandHandler(IAppDb appDb) : base(appDb)
        {
            _appDb = appDb;
        }
    }
}
