using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.ViewComponents.Shared
{
    public class SearchComponentVm
    {
        public List<SearchCategoryItem> Items { get; set; } = new List<SearchCategoryItem>();
    }

    public class SearchCategoryItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
}
