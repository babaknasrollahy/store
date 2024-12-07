using Store.Domain.Entities.baseEntities;

namespace Store.Domain.Entities.Products
{
    public class ProductFeature : BaseEntity
    {
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}
