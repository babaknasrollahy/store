using Store.Domain.Entities.baseEntities;
using Store.Domain.Entities.Cart;
using Store.Domain.Entities.Finance;

namespace Store.Domain.Entities.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Visit { get; set; }
        public int Sold { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<ProductFeature> ProductFeatures { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
