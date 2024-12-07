using Store.Common;

namespace Store.Application.Services.Finanace.Commands
{
    public interface IAddOrderService
    {
        ResultDTO Execute(int RequestPayId, int UserId, int cartId, string Address);
    }
}
