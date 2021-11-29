using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Domain.Entities
{
    public class Size : BaseEntity<Guid, User, string>
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public override bool Search(object obj)
        {
            return Title.Contains(obj.ToString()) || Code.Contains(obj.ToString());
        }
    }
}
