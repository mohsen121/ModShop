using System.ComponentModel.DataAnnotations;

namespace ModShop.RazorPages.Pages._Adminstrator.ViewComponents
{
    public enum ColumnDataType
    {
        [Display(Description = "text")]
        Text,
        [Display(Description = "number")]
        Number,
        Date,
        CheckBox,
        Select,
    }
}
