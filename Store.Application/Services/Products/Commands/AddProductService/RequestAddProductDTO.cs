using Microsoft.AspNetCore.Http;

namespace Store.Application.Services.Products.Commands.AddProductService
{
    public class RequestAddProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool Displayed { get; set; }

        public int CategoryId { get; set; }

        public List<IFormFile> ProductImages { get; set; }

        public List<ProductFeatureDTO> ProductFeatures { get; set; }
    }
}
