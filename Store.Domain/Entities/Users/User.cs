using Store.Domain.Entities.Finance;
using Store.Domain.Entities.Wallet;

namespace Store.Domain.Entities.Users
{
    public class User : baseEntities.BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<UserInRole> UserInRoles { get; set; }
        public virtual ICollection<Cart.Cart> Carts { get; set; }
        public virtual ICollection<RequestPay> RequestPays { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public virtual PersonalWallet PersonalWallet { get; set; }
        public Guid PersonalWalletId { get; set; }

        public virtual CompanyWallet CompanyWallet { get; set; }
        public Guid CompanyWalletId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
