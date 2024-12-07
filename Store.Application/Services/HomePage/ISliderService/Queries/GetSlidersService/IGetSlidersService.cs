using Store.Common;

namespace Store.Application.Services.HomePage.ISliderService.Queries.GetSlidersService
{
    public interface IGetSlidersService
    {
        ResultDTO<List<SliderDTO>> Execute();
        ResultDTO<SliderDTO> Execute(int id);
    }
}
