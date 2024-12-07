using Store.Common;

namespace Store.Application.Services.Finanace.Queries
{
    public interface IGetPayRequestService
    {
        ResultDTO<PayRequestDTO> Execute(Guid guid);
    }
}
