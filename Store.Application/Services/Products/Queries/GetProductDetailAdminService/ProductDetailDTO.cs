namespace Store.Application.Services.Products.Queries.GetProductDetailAdminService;

public class ProductDetailDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Brand { get; set; }
    public int Price { get; set; }
    public int Inventory { get; set; }
    public bool Displayed { get; set; }

    public string Category { get; set; }

    public List<ProductImageDetailDTO> ProductImages { get; set; }

    public List<ProductFeatrueDetailDTO> ProductFeatures { get; set; }
}