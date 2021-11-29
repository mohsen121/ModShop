using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Common.Util
{
    public static class UrlTools
    {
        public static bool IsAbsoluteUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
