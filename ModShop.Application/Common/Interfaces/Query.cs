using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModShop.Application.Common.Interfaces
{

    public abstract class SingleQuery<TKey, TResponse> : IRequest<TResponse>

    {
        public TKey Key { get; set; }
    }

    public abstract class AllQuery<TResponseCollection, TResponse> : IRequest<TResponseCollection>
        where TResponseCollection : IEnumerable<TResponse>
    {

    }

    public abstract class PagedQuery<TResponseCollection, TResponse> : IRequest<TResponseCollection>
        where TResponseCollection : IEnumerable<TResponse>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public string OrderColumn { get; set; }
        public string OrderDir { get; set; }
        public string Search { get; set; }
        public Dictionary<string, string[]> Filters { get; set; }
    }

    public abstract class PagedList<TEntity> : PagedQuery<List<TEntity>, TEntity>
    {

    }



    public abstract class TotalCountQuery<TEntity> : IRequest<int>

    {

    }
}
