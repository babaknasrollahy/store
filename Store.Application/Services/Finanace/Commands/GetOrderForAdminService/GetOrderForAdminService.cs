using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Domain.Entities.Finance;

namespace Store.Application.Services.Finanace.Commands.GetOrderForAdminService
{
    public class GetOrderForAdminService : IGetOrderForAdminService
    {
        private readonly IStoreContext _db;
        public GetOrderForAdminService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO<OrdersDTO> Execute(OrderState orderState, int pageSize, int page)
        {
            int row;
            var orders = _db.Orders
                .Include(c => c.User)
                .Include(c => c.RequestPay)
                .Where(c => c.OrderState == orderState).Select(c => new AdminOrderDTO()
                {
                    Address = c.Address,
                    Id = c.Id,
                    UserId = c.UserId,
                    OrderState = c.OrderState,
                    DateTime = c.InsertTime,
                    RequestPayId = c.RequestPayId,
                }).ToPaged(page, pageSize, out row);

            return new ResultDTO<OrdersDTO>()
            {
                Data = new OrdersDTO()
                {
                    Orders = orders.ToList(),
                    Page = page,
                    PageSize = pageSize,
                    Row = row
                },
                isSuccess = true,
            };
        }
    }
}
