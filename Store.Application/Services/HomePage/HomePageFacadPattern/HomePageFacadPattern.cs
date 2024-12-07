using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.HomePage.Commads.AddImageService;
using Store.Application.Services.HomePage.Commads.DeleteImageService;
using Store.Application.Services.HomePage.ISliderService.SliderFacadPattern;
using Store.Application.Services.HomePage.Queries.GetAllImagesService;

namespace Store.Application.Services.HomePage.HomePageFacadPattern
{
    public class HomePageFacadPattern : IHomePageFacadPattern
    {
        private readonly IStoreContext _db;
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _en;
        public HomePageFacadPattern(IStoreContext db, Microsoft.Extensions.Hosting.IHostEnvironment en)
        {
            _db = db;
            _en = en;
        }

        private SliderFacadPattern _slider;
        public ISliderFacadPattern Slider => _slider = _slider ?? new SliderFacadPattern(_db, _en);


        private AddImageService _addImageService;
        public IAddImageService AddImage => _addImageService = _addImageService ?? new AddImageService(_db, _en);


        private GetAllImagesService _getAllImageService;
        public IGetAllImagesService GetAllImages => _getAllImageService = _getAllImageService ?? new GetAllImagesService(_db);


        private DeleteImageService _deleteImageService;
        public IDeleteImageService DeleteImage => _deleteImageService = _deleteImageService ?? new DeleteImageService(_db, _en);
    }
}
