using System.ComponentModel.DataAnnotations;

namespace ModShop.RazorPages.Pages._Adminstrator.ViewComponents
{
    public enum ColumnFilterType
    {
        [Display(Prompt = "{name} == @0", Name = "==\"{value}\"")]
        Equality,
        [Display(Prompt = ".ToString().Contains(@0)", Name = ".ToString().Contains(\"{value}\")")]
        NullableEquality,
        [Display(Prompt = "{name}.Contains(@0)", Name = ".Contains(\"{value}\")")]
        Containing,
        [Display(Prompt = "{name} >= @0 && {name} <= @1")]
        Interval
    }
}
