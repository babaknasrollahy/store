namespace Store.Application.Services.Products.Queries.GetCategoriesService
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasChild { get; set; }
        public ParentCategoryDTO parentCategory { get; set; }
    }
}
