using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Domain.Entities.Finance;

namespace Store.Application.Services.Finanace.Commands
{
    public class AddOrderService : IAddOrderService
    {
        private readonly IStoreContext _db;
        public AddOrderService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO Execute(int RequestPayId, int UserId, int cartId, string Address)
        {
            var user = _db.Users.Find(UserId);
            var request = _db.RequestPays.Find(RequestPayId);
            var cart = _db.Carts.Include(c => c.CartItems).ThenInclude(c => c.Product).SingleOrDefault(c => c.Id == cartId);
            if (user == null || request == null || cart == null)
            {
                return new ResultDTO()
                {
                    isSuccess = false,
                    Message = "مشکلی پیش آمده است. مبلغ واریزی شما طی 24 ساعت آینده به حساب شما برخواهد گشت."
                };
            }
            request.IsPayed = true;
            cart.IsFinished = true;

            var order = new Order()
            {
                Address = Address,
                OrderState = OrderState.Processing,
                RequestPay = request,
                RequestPayId = RequestPayId,
                User = user,
                UserId = UserId,
            };
            _db.Orders.Add(order);

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cart.CartItems)
            {
                var temp = new OrderDetail()
                {
                    Count = item.Count,
                    Order = order,
                    OrderId = order.Id,
                    Product = item.Product,
                    ProductId = item.Product.Id,
                    Price = item.Product.Price,
                };
                orderDetails.Add(temp);
            }
            _db.OrderDetails.AddRange(orderDetails);

            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
            };
        }
    }
}
