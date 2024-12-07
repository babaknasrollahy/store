namespace Store.Application.Services.Products.Queries.GetProductAdminService
{
    public class ProductAdminDTO
    {
        public int CurrentPage { get; set; }
        public int RowCount { get; set; }
        public int PageSize { get; set; }

        public List<ProductDTO> Products { get; set; }
    }
}
