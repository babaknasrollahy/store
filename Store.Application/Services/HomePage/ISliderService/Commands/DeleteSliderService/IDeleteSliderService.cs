using Store.Common;

namespace Store.Application.Services.HomePage.ISliderService.Commands.DeleteSliderService
{
    public interface IDeleteSliderService
    {
        ResultDTO Execute(int id);
    }
}
