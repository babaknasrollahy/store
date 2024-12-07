namespace Store.Application.Services.Wallet.Shared.Queries.GetWalletByIdService
{
    public class WalletDTO
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

        public List<TransactionDTO> Transactions { get; set; }

        public int CurrentPage { get; set; }
        public int Row { get; set; }
    }
}
