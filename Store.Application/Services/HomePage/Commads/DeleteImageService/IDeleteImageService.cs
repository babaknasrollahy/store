using Store.Common;

namespace Store.Application.Services.HomePage.Commads.DeleteImageService
{
    public interface IDeleteImageService
    {
        ResultDTO Execute(int Id);
    }
}
