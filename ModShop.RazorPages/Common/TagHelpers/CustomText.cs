using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModShop.RazorPages.Common.TagHelpers
{
    [HtmlTargetElement(Attributes = "customtxt")]
    public class CustomText : TagHelper
    {
        public string Type { get; set; } = "text";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("customtxt");
            output.Attributes.Add("class", "form-controll");
            output.Attributes.Add("type", Type);

        }
    }
}
