using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Common.Util
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string str) =>
         string.IsNullOrEmpty(str) || str.Length < 2
         ? str
         : char.ToLowerInvariant(str[0]) + str.Substring(1);
    }
}
