using Store.Application.Services.HomePage.ISliderService;
using Store.Application.Services.HomePage.Queries.GetAllImagesService;
using Store.Application.Services.Products.Queries.GetProductUserService;

namespace EndPoint.WebSite.Models.ViewModels
{
    public class HomePageViewModel
    {
        public List<SliderDTO> Sliders { get; set; }
        public List<HomePageImageDTO> Images { get; set; }
        public List<ProductUserDTO> HandsFree { get; set; }
        public List<ProductUserDTO> Mobiles { get; set; }
    }
}
