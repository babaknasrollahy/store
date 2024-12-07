using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common.Queries.GetCategoriesUserService;

namespace EndPoint.WebSite.ViewComponents
{
    public class GetCategoriesViewComponent : ViewComponent
    {
        private readonly IGetCategoriesUserService _getCategoriesUserService;
        public GetCategoriesViewComponent(IGetCategoriesUserService getCategoriesUserService)
        {
            _getCategoriesUserService = getCategoriesUserService;
        }
        public IViewComponentResult Invoke()
        {
            return View(viewName: "Category", _getCategoriesUserService.Execute().Data);
        }
    }
}
