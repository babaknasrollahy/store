using Store.Common;

namespace Store.Application.Services.Finanace.Commands.GetOrderService
{
    public interface IGetOrderService
    {
        ResultDTO<List<OrderDTO>> Execute(int userId);
    }
}
