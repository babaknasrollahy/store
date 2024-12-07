

using Store.Application.Services.Wallet.Shared.Queries.GetWalletByIdService;

namespace Store.Application.Services.Wallet.Transaction.Commands.CreateTransactionService
{
    public class CreateTransactionDTO
    {
        public int UserId { get; set; }
        public Guid WalletId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
