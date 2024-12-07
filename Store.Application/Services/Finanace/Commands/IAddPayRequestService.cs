using Store.Common;

namespace Store.Application.Services.Finanace.Commands
{
    public interface IAddPayRequestService
    {
        public ResultDTO<PayRequestResDTO> Execute(int UserId, int Amount);
    }
}
