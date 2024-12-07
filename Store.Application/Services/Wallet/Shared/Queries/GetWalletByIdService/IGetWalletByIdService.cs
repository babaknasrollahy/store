using Store.Common;

namespace Store.Application.Services.Wallet.Shared.Queries.GetWalletByIdService
{
    public interface IGetWalletByIdService
    {
        Task<ResultDTO<WalletDTO>> ExecuteAsync(Guid Id, int page);
    }
}
