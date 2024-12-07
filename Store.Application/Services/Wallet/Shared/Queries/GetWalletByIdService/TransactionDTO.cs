

namespace Store.Application.Services.Wallet.Shared.Queries.GetWalletByIdService
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
