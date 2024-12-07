using Store.Domain.Entities.baseEntities;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Wallet
{
    public class Wallet : BaseEntity<Guid>
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
