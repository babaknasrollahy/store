namespace Store.Application.Services.Products.Queries.GetProductDetailAdminService;

public class ProductFeatrueDetailDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public string Name { get; set; }
    public string Value { get; set; }
}