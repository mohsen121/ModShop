using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Domain.Entities
{
    public class ErrorLog : BaseEntity<Guid>
    {
        public string Error { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }

        public override bool Search(object obj)
        {
            return Error.Contains(obj.ToString()) || ClassName.Contains(obj.ToString()) || MethodName.Contains(obj.ToString());
        }
    }
}
