using Store.Domain.Entities.HomePage;

namespace Store.Application.Services.HomePage.Queries.GetAllImagesService
{
    public class HomePageImageDTO
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string Link { get; set; }
        public ImageLocation Location { get; set; }
    }
}
