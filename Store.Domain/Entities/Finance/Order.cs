using Store.Domain.Entities.baseEntities;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Finance
{
    public class Order : BaseEntity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public virtual RequestPay RequestPay { get; set; }
        public int RequestPayId { get; set; }

        public string Address { get; set; }

        public OrderState OrderState { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
