using Store.Common;

namespace Store.Application.Services.Common.Queries.GetCategoriesUserService
{
    public interface IGetCategoriesUserService
    {
        public ResultDTO<List<CategoryResultDTO>> Execute();
    }
}
