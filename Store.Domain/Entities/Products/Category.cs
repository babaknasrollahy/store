using Store.Domain.Entities.baseEntities;

namespace Store.Domain.Entities.Products
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual Category ParentCategory { get; set; }
        public int? ParentCategoryId { get; set; }

        public virtual ICollection<Category> ChildCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
