using Microsoft.Extensions.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPattern;
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

namespace Store.Application.Services.Products.ProductFacadPattern
{
    public class ProductFacadPattern : IProductFacadPattern
    {
        private readonly IStoreContext _context;
        private readonly IHostEnvironment _environment;
        public ProductFacadPattern(IStoreContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        private AddCategoryService _addCategoryService;
        public IAddCategoryService AddCategory => _addCategoryService = _addCategoryService ?? new AddCategoryService(_context);

        private GetCategoriesService _getCategoriesService;
        public IGetCategoriesService GetCategoties => _getCategoriesService = _getCategoriesService ?? new GetCategoriesService(_context);

        private DeleteCategoriesService _deleteCategoriesService;
        public IDeleteCategoriesService DeleteCategories => _deleteCategoriesService = _deleteCategoriesService ?? new DeleteCategoriesService(_context);

        private GetCategoryByIdService _getCategoryByIdService;
        public IGetCategoryByIdService GetCategoryById => _getCategoryByIdService = _getCategoryByIdService ?? new GetCategoryByIdService(_context);

        private EditCategoryService _editCategoryService;
        public IEditCategoryService EditCategory => _editCategoryService = _editCategoryService ?? new EditCategoryService(_context);

        private GetAllCategories _getAllCategoriesService;
        public IGetAllCategories GetAllCategories => _getAllCategoriesService = _getAllCategoriesService ?? new GetAllCategories(_context);

        private AddProductService _addProductService;
        public IAddProductService AddProduct => _addProductService = _addProductService ?? new AddProductService(_context, _environment);

        private GetProductAdmin _getProductAdminService;
        public IGetProductAdmin GetProductAdmin => _getProductAdminService = _getProductAdminService ?? new GetProductAdmin(_context);

        private GetProductDetailAdmin _getProductDetailAdminService;
        public IGetProductDetailAdminService GetProductDetailAdmin => _getProductDetailAdminService = _getProductDetailAdminService ?? new GetProductDetailAdmin(_context);

        private DeleteProductService _deleteProductService;
        public IDeleteProductService DeleteProduct => _deleteProductService = _deleteProductService ?? new DeleteProductService(_context);

        private GetProductUserService _getProductUserService;
        public IGetProductUserService GetProductUser => _getProductUserService = _getProductUserService ?? new GetProductUserService(_context);

        private GetProductDetailUserService _getProductDetailUserService;
        public IGetProductDetailUserSerivce GetProductDetailUser => _getProductDetailUserService = _getProductDetailUserService ?? new GetProductDetailUserService(_context);
    }
}
