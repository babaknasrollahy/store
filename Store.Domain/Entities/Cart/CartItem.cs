using Store.Domain.Entities.baseEntities;
using Store.Domain.Entities.Products;

namespace Store.Domain.Entities.Cart
{
    public class CartItem : BaseEntity
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }

        public virtual Cart Cart { get; set; }
        public int CartId { get; set; }
    }
}
