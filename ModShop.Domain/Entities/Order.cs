using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModShop.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int OrderNumber { get; set; }
        public double TotalPrice { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double Tax { get; set; }
        public double Discount { get; set; }

        public bool IsPaid { get; set; }
        public DateTime? PayTime { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }
        public override bool Search(object obj)
        {
            return UserId.ToString().Contains(obj.ToString());
        }
    }
}
