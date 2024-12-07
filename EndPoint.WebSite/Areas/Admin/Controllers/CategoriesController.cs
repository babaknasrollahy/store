using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPattern;
using Store.Common;

namespace EndPoint.WebSite.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin, Operator")]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IProductFacadPattern _product;
        public CategoriesController(IProductFacadPattern context)
        {
            _product = context;
        }
        public IActionResult Index(int? ParentId)
        {
            ViewBag.parentId = ParentId;
            return View(_product.GetCategoties.Execute(ParentId).Data);
        }

        [HttpGet]
        public IActionResult Create(int? ParentId)
        {
            ViewBag.parentId = ParentId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(string Name, int? ParentId)
        {
            var valid = IsValidRequest(Name, "لطفا نام دسته بندی را وارد کنید!");
            if (!valid.isSuccess)
                return Json(valid);

            return Json(_product.AddCategory.Execute(ParentId, Name));
        }
        private ResultDTO IsValidRequest(string s, string Massege)
        {
            if (string.IsNullOrWhiteSpace(s))
                return new Store.Common.ResultDTO
                {
                    isSuccess = false,
                    Message = Massege,
                };

            return new Store.Common.ResultDTO
            {
                isSuccess = true,
                Message = "عملیات با موفقیت انجام شد!",
            };

        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            return Json(_product.DeleteCategories.Execute(Id));
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = _product.GetCategoryById.Execute(Id);
            if (category.isSuccess)
            {
                ViewBag.Id = Id;
                ViewBag.Name = category.Data.Name;
                return View();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int Id, string Name)
        {
            return Json(_product.EditCategory.Execute(Id, Name));
        }
    }
}
