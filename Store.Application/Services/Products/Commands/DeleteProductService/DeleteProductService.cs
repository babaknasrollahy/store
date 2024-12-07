using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Products.Commands.DeleteProductService
{
    public class DeleteProductService : IDeleteProductService
    {
        private readonly IStoreContext _db;
        public DeleteProductService(IStoreContext context)
        {
            _db = context;
        }

        public ResultDTO Execute(int Id)
        {
            var Product = _db.Products
                .Include(c => c.ProductImages)
                .Include(c => c.ProductFeatures)
                .FirstOrDefault(c => c.Id == Id);
            if (Product == null)
                return new ResultDTO() { isSuccess = false, Message = "محصول یافت نشد!" };

            if (Product.ProductFeatures != null)
                _db.ProductFeatures.RemoveRange(Product.ProductFeatures);
            if (Product.ProductImages != null)
                foreach (var item in Product.ProductImages)
                {
                    System.IO.File.Delete(item.Src);
                    _db.ProductImages.Remove(item);
                }

            _db.Products.Remove(Product);
            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
                Message = "محصول مورد نظر به طور کامل حذف شد!",
            };
        }
    }
}
