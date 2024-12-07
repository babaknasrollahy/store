using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.Products.Queries.GetProductUserService;

namespace EndPoint.WebSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductFacadPattern _product;
        public ProductsController(IProductFacadPattern product)
        {
            _product = product;
        }
        public IActionResult Index(string? SearchKey, int? CategoryId = null, Ordering ordering = Ordering.MostSold, int page = 1, int pageSize = 20)
        {
            TempData["Page"] = page;
            TempData["PageSize"] = pageSize;
            TempData["QueryString"] = GetQueryString(nameof(SearchKey), nameof(CategoryId), nameof(page), nameof(pageSize));

            return View(_product.GetProductUser.Execute(SearchKey, CategoryId, ordering, page, pageSize).Data);
        }
        public IActionResult Detail(int id)
        {
            return View(_product.GetProductDetailUser.Execute(id).Data);
        }

        [NonAction]
        private string GetQueryString(params string[] Keys)
        {
            string queryString = string.Empty;
            if (Request.QueryString.Value == null)
                return queryString;

            foreach (var item in Keys)
            {
                if (Request.QueryString.Value.Contains(item))
                {
                    queryString += $"&{item}={Request.Query[item]}";
                }
            }
            return queryString;
        }
    }
}
