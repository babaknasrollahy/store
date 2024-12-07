using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.User.Queries.GetUsersService
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IStoreContext _db;
        public GetUsersService(IStoreContext context)
        {
            _db = context;
        }
        public UserResultDTO Execute(RequestGetUserDTO request)
        {
            var users = _db.Users
                .AsNoTracking()
                .Include(u => u.CompanyWallet)
                .ThenInclude(w => w.Transactions)
                .Include(u => u.PersonalWallet)
                .ThenInclude(w => w.Transactions)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(c => c.Name.ToLower().Contains(request.SearchKey) || c.Email.ToLower().Contains(request.SearchKey));
            }

            int rows;
            var userList = users.ToPaged(request.Page, 50, out rows).Select(c => new UserDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                IsActive = c.IsActive,
                CompanyWallet = new GetUserWalletDTO()
                {
                    Id = c.CompanyWallet.Id,
                    Balance = c.CompanyWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.InCome).Sum(c => c.Amount) - c.CompanyWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.OutCome).Sum(c => c.Amount),
                },
                PersonalWallet = new GetUserWalletDTO()
                {
                    Id = c.PersonalWallet.Id,
                    Balance = c.PersonalWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.InCome).Sum(c => c.Amount) - c.PersonalWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.OutCome).Sum(c => c.Amount),
                }
            }).ToList();

            return new UserResultDTO
            {
                Users = userList,
                Rows = rows,
            };
        }
    }
}
