using Store.Application.Services.Products.Commands.AddCategoryService;
using Store.Application.Services.Products.Commands.AddProductService;
using Store.Application.Services.Products.Commands.DeleteCategoriesService;
using Store.Application.Services.Products.Commands.DeleteProductService;
using Store.Application.Services.Products.Commands.EditCategoryService;
using Store.Application.Services.Products.Queries.GetAllCatgories;
using Store.Application.Services.Products.Queries.GetCategoriesService;
using Store.Application.Services.Products.Queries.GetCategoryByIdService;
using Store.Application.Services.Products.Queries.GetProductAdminService;
using Store.Application.Services.Products.Queries.GetProductDetailAdminService;
using Store.Application.Services.Products.Queries.GetProductDetailUserService;
using Store.Application.Services.Products.Queries.GetProductUserService;

namespace Store.Application.Interfaces.FacadPattern
{
    public interface IProductFacadPattern
    {
        IAddCategoryService AddCategory { get; }
        IGetCategoriesService GetCategoties { get; }
        IGetCategoryByIdService GetCategoryById { get; }
        IDeleteCategoriesService DeleteCategories { get; }
        IEditCategoryService EditCategory { get; }
        IGetAllCategories GetAllCategories { get; }
        IAddProductService AddProduct { get; }
        IGetProductAdmin GetProductAdmin { get; }
        IGetProductDetailAdminService GetProductDetailAdmin { get; }
        IDeleteProductService DeleteProduct { get; }
        IGetProductUserService GetProductUser { get; }
        IGetProductDetailUserSerivce GetProductDetailUser { get; }
    }
}
