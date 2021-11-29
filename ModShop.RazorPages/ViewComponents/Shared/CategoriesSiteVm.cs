using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.ViewComponents.Shared
{
    public class CategoriesSiteVm
    {
        public List<CategoryItem> Categories { get; set; }
    }

    public class CategoryItem
    {
        public string Title { get; set; }
        public Guid Id { get; set; }
        public CategoryItem Parent { get; set; }
        public List<CategoryItem> Children { get; set; }
    }
}
