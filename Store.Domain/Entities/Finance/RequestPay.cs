using Store.Domain.Entities.baseEntities;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Finance
{
    public class RequestPay : BaseEntity
    {
        public Guid Guid { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }

        public int Amount { get; set; }

        public bool IsPayed { get; set; }
        public DateTime? PayTime { get; set; }

        public string? Authority { get; set; }
        public long RefId { get; set; } = 0;

        public virtual Order Order { get; set; }
        public int? OrderId { get; set; }
    }
}
