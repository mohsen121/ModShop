using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Common.Models
{
    public class PaginationModel<TModel> where TModel : class
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public IList<TModel> Items { get; set; }



    }
}
