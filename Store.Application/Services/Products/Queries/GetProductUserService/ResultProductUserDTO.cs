namespace Store.Application.Services.Products.Queries.GetProductUserService
{
    public class ResultProductUserDTO
    {
        public List<ProductUserDTO> products { get; set; }
        public int TotalRows { get; set; }
    }
}
