using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Common.Util
{
    public static class ExceptionTools
    {
        public static string GetExceptionMessage(this Exception ex)
        {
            var error = ex.Message;
            while (ex.InnerException != null)
            {
                error += $"\n-->{ex.InnerException.Message}";
                ex = ex.InnerException;
            }

            return error;
        }
    }
}
