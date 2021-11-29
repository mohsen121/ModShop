using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Application.Common.Interfaces
{
    public abstract class Command : Command<Unit>
    {
    }

    public abstract class Command<TResponse> : IRequest<TResponse>
    {

    }
}
