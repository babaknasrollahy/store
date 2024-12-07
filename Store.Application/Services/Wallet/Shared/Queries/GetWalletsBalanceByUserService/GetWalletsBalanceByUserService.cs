using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Wallet.Shared.Queries.GetWalletsBalanceByUserService
{
    public class GetWalletsBalanceByUserService : IGetWalletsBalanceByUserService
    {
        private readonly IStoreContext _db;
        public GetWalletsBalanceByUserService(IStoreContext db)
        {
            _db = db;
        }
        public async Task<ResultDTO<WalletsBalancesDTO>> ExecuteAsync(int userId)
        {
            var walletsBalances = new WalletsBalancesDTO();
            var companyWallet = await _db.CompanyWallets.Include(c => c.Transactions).FirstOrDefaultAsync(c => c.UserId == userId);
            walletsBalances.CompanyBalance = (int)companyWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.InCome).Sum(c => c.Amount) - (int)companyWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.OutCome).Sum(c => c.Amount);

            var personalWallet = await _db.PersonalWallets.Include(c => c.Transactions).FirstOrDefaultAsync(c => c.UserId == userId);
            walletsBalances.PersonalBalance = (int)personalWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.InCome).Sum(c => c.Amount) - (int)personalWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.OutCome).Sum(c => c.Amount);

            return new ResultDTO<WalletsBalancesDTO>()
            {
                isSuccess = true,
                Data = walletsBalances,
                Message = "Done!"
            };
        }
    }
}
