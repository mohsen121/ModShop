using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Pages._Adminstrator.ViewComponents.Common
{
    public class SidebarViewModel
    {
        public string Logo { get; set; }
        public List<GroupMenu> Menus { get; set; }
    }

    public class GroupMenu
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public List<GroupMenuItem> Items { get; set; }
    }

    public class GroupMenuItem
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool IsParent { get; set; }
        public bool IsRoot { get; set; }
        public string Url { get; set; }
        public List<GroupMenuItem> Children { get; set; }
    }
}
