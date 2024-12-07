using Store.Domain.Entities.HomePage;

namespace Store.Application.Services.HomePage.Commads.AddImageService
{
    public class ImageDTO
    {
        public string Link { get; set; }
        public ImageLocation ImageLocation { get; set; }
    }
}
