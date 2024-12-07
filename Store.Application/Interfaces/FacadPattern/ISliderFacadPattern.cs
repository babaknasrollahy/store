using Store.Application.Services.HomePage.ISliderService.Commands.AddSliderService;
using Store.Application.Services.HomePage.ISliderService.Commands.DeleteSliderService;
using Store.Application.Services.HomePage.ISliderService.Queries.GetSlidersService;

namespace Store.Application.Interfaces.FacadPattern
{
    public interface ISliderFacadPattern
    {
        IGetSlidersService GetSliders { get; }
        IAddSliderService AddSlider { get; }
        IDeleteSliderService DeleteSlider { get; }
    }
}
