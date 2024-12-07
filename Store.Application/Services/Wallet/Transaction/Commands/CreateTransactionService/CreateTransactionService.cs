using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Domain.Entities.Wallet;

namespace Store.Application.Services.Wallet.Transaction.Commands.CreateTransactionService
{
    public class CreateTransactionService : ICreateTransactionService
    {
        private readonly IStoreContext _db;

        public CreateTransactionService(IStoreContext db)
        {
            _db = db;
        }

        public async Task<ResultDTO> ExecuteAsync(CreateTransactionDTO transactionDTO)
        {
            var transacrion = new Domain.Entities.Wallet.Transaction()
            {
                Id = Guid.NewGuid(),
                UserId = transactionDTO.UserId,
                WalletId = transactionDTO.WalletId,
                Amount = transactionDTO.Amount,
                TransactionType = (TransactionType)transactionDTO.TransactionType
            };

            await _db.Transactions.AddAsync(transacrion);
            await _db.SaveChangesAsync();

            return new ResultDTO()
            {
                isSuccess = true,
                Message = "Transaction created successfully"
            };
        }
    }
}
