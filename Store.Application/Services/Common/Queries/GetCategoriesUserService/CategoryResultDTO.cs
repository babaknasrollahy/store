namespace Store.Application.Services.Common.Queries.GetCategoriesUserService
{
    public class CategoryResultDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryResultDTO> Children { get; set; }
    }
}
