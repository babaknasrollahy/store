using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Queries.GetCategoryByIdService
{
    public class GetCategoryByIdService : IGetCategoryByIdService
    {
        private readonly IStoreContext _db;
        public GetCategoryByIdService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO<CategoryDTO> Execute(int id)
        {
            var c = _db.Categories.Find(id);
            if (c == null)
                return new ResultDTO<CategoryDTO>()
                {
                    isSuccess = false,
                    Message = "دسته بندی یافت نشد!"
                };
            return new ResultDTO<CategoryDTO>()
            {
                isSuccess = true,
                Message = "عملیات با موفقیت انجام شد!",
                Data = new CategoryDTO() { Name = c.Name },
            };
        }
    }
}
