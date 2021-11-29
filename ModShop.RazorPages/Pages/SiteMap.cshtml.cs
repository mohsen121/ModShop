using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ModShop.RazorPages.Pages
{
    public class SiteMapModel : PageModel
    {
        [BindProperty]
        public List<SiteMapVM> Input { get; set; }
        public IActionResult OnGet()
        {
            var host = HttpContext.Request.Host.Value;
            Input = new List<SiteMapVM>
            {
                new SiteMapVM
                {
                    Url = $"{host}",
                    ChangeFreq = "monthly",
                    LastModified = new DateTime(2021, 3, 14).ToUniversalTime().ToString("s") + "Z"
                },
                new SiteMapVM
                {
                    Url = $"{host}/resume",
                    ChangeFreq = "monthly",
                    LastModified = new DateTime(2021, 3, 14).ToUniversalTime().ToString("s") + "Z"
                },
            };

            return Page();
        }
    }

    public class SiteMapVM
    {
        public string Url { get; set; }
        public string ChangeFreq { get; set; }
        public string LastModified { get; set; }
    }
}
