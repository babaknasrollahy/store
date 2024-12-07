using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Common.Queries.GetCategoriesSearchService;

namespace EndPoint.WebSite.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly IGetCategoriesSearchService _getCategoriesSearchService;
        public SearchViewComponent(IGetCategoriesSearchService getCategoriesSearchService)
        {
            _getCategoriesSearchService = getCategoriesSearchService;
        }
        public IViewComponentResult Invoke()
        {
            return View(viewName: "Search", _getCategoriesSearchService.Execute().Data);
        }
    }
}
