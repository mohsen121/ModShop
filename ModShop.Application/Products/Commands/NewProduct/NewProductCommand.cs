using ModShop.Application.Common.Interfaces;
using ModShop.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ModShop.Application.BaseEntities.Commands.CreateBaseEntity;

namespace ModShop.Application.Products.Commands.NewProduct
{
    public class NewProductCommand : ICreateBaseEntityCommand<Product>
    {
        public NewProductCommand()
        {
            Entity = new Product();
        }
        public Product Entity { get; set; }
    }

    public class NewProductCommandHandler : CreateBaseEntityCommandHandler<Product, NewProductCommand>
    {
        public NewProductCommandHandler(IAppDb appDb) : base(appDb)
        {
        }
    }
}
