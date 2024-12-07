namespace Store.Application.Services.Products.Queries.GetProductDetailUserService
{
    public class ProductDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public string Category { get; set; }
        public List<string> Images { get; set; }
        public List<ProductFeatureDTO> ProductFeature { get; set; }
    }
}
