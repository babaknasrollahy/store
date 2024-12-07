using Store.Common;

namespace Store.Application.Services.HomePage.ISliderService.Commands.AddSliderService
{
    public interface IAddSliderService
    {
        ResultDTO Execute(SliderUploadFileDTO slider);
    }
}
