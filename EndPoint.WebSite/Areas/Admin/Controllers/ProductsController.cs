using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.Products.Commands.AddProductService;

namespace EndPoint.WebSite.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin, Operator")]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductFacadPattern _product;
        public ProductsController(IProductFacadPattern product)
        {
            _product = product;
        }
        public IActionResult Index(int page = 1, int pageSize = 20)
        {
            return View(_product.GetProductAdmin.Execute(page, pageSize).Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_product.GetAllCategories.Execute().Data, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(RequestAddProductDTO request)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count; i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.ProductImages = images;
            return Json(_product.AddProduct.Execute(request));
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            return View(_product.GetProductDetailAdmin.Execute(id).Data);
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            return View(_product.DeleteProduct.Execute(Id));
        }
    }
}
