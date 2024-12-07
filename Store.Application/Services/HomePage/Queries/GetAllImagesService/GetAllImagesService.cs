using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.HomePage.Queries.GetAllImagesService
{
    public class GetAllImagesService : IGetAllImagesService
    {
        private readonly IStoreContext _db;
        public GetAllImagesService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO<List<HomePageImageDTO>> Execute()
        {
            return new ResultDTO<List<HomePageImageDTO>>()
            {
                isSuccess = true,
                Data = _db.HomePageImages.Select(x => new HomePageImageDTO()
                {
                    Id = x.Id,
                    Link = x.Link,
                    Location = x.ImageLocation,
                    Src = x.Src
                }).ToList(),
            };
        }
    }
}
