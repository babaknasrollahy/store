using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities.Cart;
using Store.Domain.Entities.Finance;
using Store.Domain.Entities.HomePage;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;
using Store.Domain.Entities.Wallet;

namespace Store.Application.Interfaces.Contexts
{
    public interface IStoreContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductFeature> ProductFeatures { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<Slides> Slides { get; set; }
        DbSet<HomePageImages> HomePageImages { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<CartItem> CartItems { get; set; }
        DbSet<RequestPay> RequestPays { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }

        DbSet<PersonalWallet> PersonalWallets { get; set; }
        DbSet<CompanyWallet> CompanyWallets { get; set; }
        DbSet<Transaction> Transactions { get; set; }

        int SaveChanges(bool accepAllChangesOnSuccess);
        int SaveChanges();
        Task<int> SaveChangesAsync(bool accepAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
