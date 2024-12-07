using Store.Common;

namespace Store.Application.Services.Wallet.Shared.Queries.GetWalletsBalanceByUserService
{
    public interface IGetWalletsBalanceByUserService
    {
        Task<ResultDTO<WalletsBalancesDTO>> ExecuteAsync(int userId);
    }
}
