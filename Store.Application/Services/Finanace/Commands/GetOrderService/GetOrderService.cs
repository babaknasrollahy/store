using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Finanace.Commands.GetOrderService
{
    public class GetOrderService : IGetOrderService
    {
        private readonly IStoreContext _db;
        public GetOrderService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO<List<OrderDTO>> Execute(int userId)
        {
            var orders = _db.Orders
                .Include(c => c.User)
                .Include(c => c.RequestPay)
                .Include(c => c.OrderDetails)
                .ThenInclude(c => c.Product)
                .Where(c => c.UserId == userId).Select(c => new OrderDTO()
                {
                    Address = c.Address,
                    Id = c.Id,
                    OrderState = c.OrderState,
                    RequestPayId = c.RequestPayId,
                    OrderDetails = c.OrderDetails.Select(c => new OrderDetailDTO()
                    {
                        Count = c.Count,
                        Price = c.Price,
                        ProductName = c.Product.Name,
                        ProductImg = c.Product.ProductImages.First().Src
                    }).ToList(),
                }).ToList();
            return new ResultDTO<List<OrderDTO>>()
            {
                Data = orders,
                isSuccess = true,
            };
        }
    }
}
