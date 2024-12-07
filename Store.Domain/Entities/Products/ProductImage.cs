using Store.Domain.Entities.baseEntities;

namespace Store.Domain.Entities.Products
{
    public class ProductImage : BaseEntity
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public string Src { get; set; }
    }
}
