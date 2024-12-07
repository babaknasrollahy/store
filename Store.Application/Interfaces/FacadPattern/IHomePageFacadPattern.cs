using Store.Application.Services.HomePage.Commads.AddImageService;
using Store.Application.Services.HomePage.Commads.DeleteImageService;
using Store.Application.Services.HomePage.Queries.GetAllImagesService;

namespace Store.Application.Interfaces.FacadPattern
{
    public interface IHomePageFacadPattern
    {
        public ISliderFacadPattern Slider { get; }
        public IAddImageService AddImage { get; }
        public IGetAllImagesService GetAllImages { get; }
        public IDeleteImageService DeleteImage { get; }

    }
}
