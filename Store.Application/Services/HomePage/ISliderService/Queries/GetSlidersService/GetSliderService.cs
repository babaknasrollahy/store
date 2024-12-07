using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.HomePage.ISliderService.Queries.GetSlidersService
{
    public class GetSliderService : IGetSlidersService
    {
        private readonly IStoreContext _db;
        public GetSliderService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO<List<SliderDTO>> Execute()
        {
            var sliders = _db.Slides.Select(c => new SliderDTO()
            {
                Id = c.Id,
                Link = c.Link,
                Src = c.Src,
            }).ToList();
            return new ResultDTO<List<SliderDTO>>()
            {
                isSuccess = true,
                Data = sliders
            };
        }
        public ResultDTO<SliderDTO> Execute(int Id)
        {
            var slider = _db.Slides.Find(Id);
            if (slider == null)
            {
                return new ResultDTO<SliderDTO>()
                {
                    isSuccess = false
                };
            }

            return new ResultDTO<SliderDTO>()
            {
                isSuccess = true,
                Data = new SliderDTO()
                {
                    Id = Id,
                    Src = slider.Src,
                    Link = slider.Link,
                }
            };
        }
    }
}
