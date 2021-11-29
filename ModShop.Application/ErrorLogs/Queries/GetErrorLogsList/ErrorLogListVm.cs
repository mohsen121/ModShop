using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Application.ErrorLogs.Queries.GetErrorLogsList
{
    public class ErrorLogListVm
    {
        public IList<ErrorLogVm> Items { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
    }

    public class ErrorLogVm
    {
        public Guid Id { get; set; }
        public string Error { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public DateTime Created { get; set; }
    }
}
