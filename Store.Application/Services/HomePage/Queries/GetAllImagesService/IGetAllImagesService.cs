using Store.Common;

namespace Store.Application.Services.HomePage.Queries.GetAllImagesService
{
    public interface IGetAllImagesService
    {
        ResultDTO<List<HomePageImageDTO>> Execute();
    }
}
