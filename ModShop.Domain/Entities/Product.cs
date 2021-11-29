using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Domain.Entities
{
    public class Product : BaseEntity<Guid, User, string>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public Guid? ColorId { get; set; }
        public virtual Color Color { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public Guid? SizeId { get; set; }
        public virtual Size Size { get; set; }

        public bool IsForSale { get; set; }
        public Guid? BaseProductId { get; set; }
        public virtual Product BaseProduct { get; set; }

        public string Image { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<Product> Children { get; set; }
        public ICollection<OrderItem> Orders { get; set; }
        public override bool Search(object obj)
        {
            var target = obj.ToString();
            return this.Title.Contains(target) || this.Description.Contains(target);
        }
    }
}
