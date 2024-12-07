using Store.Common;

namespace Store.Application.Services.Finanace.Commands.PayUsingWalletService;

public interface IPayUsingWalletService
{
    Task<ResultDTO> ExecuteAsync(int userId, bool useCompanyWallet);
}