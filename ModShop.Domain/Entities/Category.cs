using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Domain.Entities
{
    public class Category : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid? ParentId { get; set; }
        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children { get; set; }
        public override bool Search(object obj)
        {
            return Title.Contains(obj.ToString()) || Description.Contains(obj.ToString());
        }
    }
}
