using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.ViewComponents
{
    public class Select2Dto
    {
        public string AjaxUrl { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public Dictionary<string, string> Attributes { get; set; }
        public List<Select2Item> Items { get; set; }
    }

    public class Select2Item
    {
        public object Key { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }
}
