using Store.Domain.Entities.baseEntities;
using Store.Domain.Entities.Users;

namespace Store.Domain.Entities.Cart
{
    public class Cart : BaseEntity
    {
        public virtual User User { get; set; }
        public int? UserId { get; set; }

        public bool IsFinished { get; set; } = false;
        public Guid BrowserId { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = null!;
    }
}
