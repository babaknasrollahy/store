using Store.Common;
using Store.Domain.Entities.Finance;

namespace Store.Application.Services.Finanace.Commands.GetOrderForAdminService
{
    public interface IGetOrderForAdminService
    {
        ResultDTO<OrdersDTO> Execute(OrderState orderState, int pageSize, int page);
    }
}
