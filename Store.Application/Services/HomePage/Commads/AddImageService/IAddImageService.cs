using Microsoft.AspNetCore.Http;
using Store.Common;

namespace Store.Application.Services.HomePage.Commads.AddImageService
{
    public interface IAddImageService
    {
        ResultDTO Execute(IFormFile file, ImageDTO image);
    }
}
