using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Domain.Entities
{
    public class Slider : BaseEntity<Guid, User, string>
    {
        public string Image { get; set; }
        public int Order { get; set; }
        public string HtmlText { get; set; }
        public override bool Search(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
