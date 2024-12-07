using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Queries.GetProductUserService
{
    public class GetProductUserService : IGetProductUserService
    {
        private readonly IStoreContext _db;
        public GetProductUserService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO<ResultProductUserDTO> Execute(string? SearchKey, int? CategoryId, Ordering ordering, int page, int pageSize)
        {
            int Rows;
            var ProductsQuery = _db.Products
                .Include(c => c.ProductImages)
                .Include(c => c.Category)
                .AsQueryable();

            if (CategoryId != null)
                ProductsQuery = ProductsQuery.Where(c => c.CategoryId == CategoryId || c.Category.ParentCategoryId == CategoryId).AsQueryable();
            if (!string.IsNullOrWhiteSpace(SearchKey))
                ProductsQuery = ProductsQuery.Where(c => c.Name.Contains(SearchKey) || c.Brand.Contains(SearchKey) || c.Description.Contains(SearchKey)).AsQueryable();

            switch (ordering)
            {
                case Ordering.MostVisited:
                    ProductsQuery = ProductsQuery.OrderByDescending(c => c.Visit).AsQueryable();
                    break;
                case Ordering.MostSold:
                    ProductsQuery = ProductsQuery.OrderByDescending(c => c.Sold).AsQueryable();
                    break;
                case Ordering.Newest:
                    ProductsQuery = ProductsQuery.OrderByDescending(c => c.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    ProductsQuery = ProductsQuery.OrderBy(c => c.Price).AsQueryable();
                    break;
                case Ordering.MostExpensive:
                    ProductsQuery = ProductsQuery.OrderByDescending(c => c.Price).AsQueryable();
                    break;
                default:
                    ProductsQuery = ProductsQuery.OrderByDescending(c => c.Id).AsQueryable();
                    break;
            }

            var Products = ProductsQuery
            .ToPaged(page, pageSize, out Rows)
            .ToList();
            var rd = new Random();
            return new ResultDTO<ResultProductUserDTO>()
            {
                isSuccess = true,
                Message = "",
                Data = new ResultProductUserDTO()
                {
                    products = Products.Select(c => new ProductUserDTO()
                    {
                        Id = c.Id,
                        ImageSrc = c.ProductImages.FirstOrDefault() != null ? c.ProductImages.First().Src : "~/Images/ProductImages/Default.png",
                        Price = c.Price,
                        Rate = rd.Next(1, 6),
                        Title = c.Name
                    }).ToList(),
                    TotalRows = Rows
                }
            };
        }
    }
}
