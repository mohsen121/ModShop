using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ModShop.Application.Sliders.Queries.GetPagedSliders;
using ModShop.Domain.Entities;
using ModShop.RazorPages.Pages._Adminstrator.ViewComponents;

namespace ModShop.RazorPages.Pages._Adminstrator.Sliders
{
    public class SliderListModel : BaseListPage<Slider, SliderListVm, GetPagedSlidersQuery>
    {
        [BindProperty]
        public override DatatableDto Dto { get; set; }

        public SliderListModel() : base("created", "desc")
        {
        }
    }
}
