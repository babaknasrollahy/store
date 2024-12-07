using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.HomePage.ISliderService.Commands.AddSliderService;
using Store.Application.Services.HomePage.ISliderService.Commands.DeleteSliderService;
using Store.Application.Services.HomePage.ISliderService.Queries.GetSlidersService;

namespace Store.Application.Services.HomePage.ISliderService.SliderFacadPattern
{
    public class SliderFacadPattern : ISliderFacadPattern
    {
        private readonly IStoreContext _db;
        private readonly Microsoft.Extensions.Hosting.IHostEnvironment _en;
        public SliderFacadPattern(IStoreContext db, Microsoft.Extensions.Hosting.IHostEnvironment en)
        {
            _db = db;
            _en = en;
        }

        private GetSliderService _getSliderService;
        public IGetSlidersService GetSliders => _getSliderService = _getSliderService ?? new GetSliderService(_db);

        private AddSliderService _addSliderService;
        public IAddSliderService AddSlider => _addSliderService = _addSliderService ?? new AddSliderService(_db, _en);

        private DeleteSliderService _deleteSliderService;
        public IDeleteSliderService DeleteSlider => _deleteSliderService = _deleteSliderService ?? new DeleteSliderService(_db, _en);
    }
}
