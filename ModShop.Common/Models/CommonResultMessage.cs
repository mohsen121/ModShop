using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Common.Models
{
    public class CommonResultMessage<TResult> where TResult : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public TResult Result { get; set; }

    }
}
