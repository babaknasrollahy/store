using Store.Domain.Entities.baseEntities;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Wallet
{
    public class Transaction : BaseEntity<Guid>
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual Wallet Wallet { get; set; }
        public Guid WalletId { get; set; }

        public decimal Amount { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
