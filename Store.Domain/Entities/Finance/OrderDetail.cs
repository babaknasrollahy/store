using Store.Domain.Entities.baseEntities;
using Store.Domain.Entities.Products;

namespace Store.Domain.Entities.Finance
{
    public class OrderDetail : BaseEntity
    {
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }
    }

}
