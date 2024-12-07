using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Wallet.Shared.Queries.GetWalletByIdService
{
    public class GetWalletByIdService : IGetWalletByIdService
    {
        private readonly IStoreContext _db;
        public GetWalletByIdService(IStoreContext db)
        {
            _db = db;
        }

        public async Task<ResultDTO<WalletDTO>> ExecuteAsync(Guid id, int page)
        {
            Domain.Entities.Wallet.Wallet? wallet = await _db.CompanyWallets
                .AsNoTracking()
                .Include(c => c.Transactions)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (wallet == null)
                wallet = await _db.PersonalWallets.AsNoTracking()
                .Include(c => c.Transactions)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (wallet == null)
                return new ResultDTO<WalletDTO>() { isSuccess = false, Message = "Wallet not found" };

            List<TransactionDTO> transactions = _db.Transactions
                .AsNoTracking()
                .Include(t => t.User)
                .Where(t => t.WalletId == id)
                .Select(c => new TransactionDTO()
                {
                    Id = c.Id,
                    UserId = c.User.Id,
                    UserName = c.User.Name,
                    Amount = c.Amount,
                    TransactionType = (TransactionType)c.TransactionType,
                    TransactionDate = c.InsertTime,
                })
                .ToPaged(page, 20, out int row)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();


            var res = new WalletDTO()
            {
                Id = id,
                UserId = wallet.User.Id,
                UserName = wallet.User.Name,
                Transactions = transactions,
                CurrentPage = page,
                Row = row
            };

            return new ResultDTO<WalletDTO>() { isSuccess = true, Data = res, Message = "Done!" };
        }
    }
}
