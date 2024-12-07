using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Domain.Entities.Cart;
using Store.Domain.Entities.Finance;
using Store.Domain.Entities.HomePage;
using Store.Domain.Entities.Products;
using Store.Domain.Entities.Users;
using Store.Domain.Entities.Wallet;

namespace Store.Persistence.Contexts
{
    public class StoreContext : DbContext, IStoreContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Slides> Slides { get; set; }
        public DbSet<HomePageImages> HomePageImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<RequestPay> RequestPays { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<PersonalWallet> PersonalWallets { get; set; }
        public DbSet<CompanyWallet> CompanyWallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //add default value to role(admin,operator,customer)
            modelBuilder.Entity<Role>().HasData(new { Id = (int)RolesList.Admin, Name = nameof(RolesList.Admin) });
            modelBuilder.Entity<Role>().HasData(new { Id = (int)RolesList.Operator, Name = nameof(RolesList.Operator) });
            modelBuilder.Entity<Role>().HasData(new { Id = (int)RolesList.Customer, Name = nameof(RolesList.Customer) });

            //modelBuilder.Entity<User>().HasData(new { Id = 10, Name = "FarzadNasrollahy", Email = "FarzadNasrolahy@gmail.com",Password = "AQAAAAEAACcQAAAAEMABxNTmVq8Vq6Ul95Bxx5hQCxNZWpO5yv1YkH0gixEf5FVZnm6lMll/oZXLqziwHQ==", IsActive = true,IsRemoved = false, InsertTime = DateTime.Now});

            //make email unique
            modelBuilder.Entity<User>().HasIndex(c => c.Email).IsUnique();
            //add default admin user
            Guid companyWalletId = Guid.NewGuid();
            Guid personalWalletId = Guid.NewGuid();
            modelBuilder.Entity<CompanyWallet>().HasData(new { Id = companyWalletId, UserId = 1, IsRemoved = false, InsertTime = DateTime.Now });
            modelBuilder.Entity<PersonalWallet>().HasData(new { Id = personalWalletId, UserId = 1, IsRemoved = false, InsertTime = DateTime.Now });
            modelBuilder.Entity<User>().HasData(new { Id = 1, Name = "Admin", Email = "FarzadNasrolahy@gmail.com", Password = "AQAAAAEAACcQAAAAEHnFZoWnWUarSFruZpp5Kis9Rm4Jw7saVY9v9o6SuQpfE9yM9+3SggktjucAf5JnoA==", PersonalWalletId = personalWalletId, CompanyWalletId = companyWalletId, IsActive = true, IsRemoved = false, InsertTime = DateTime.Now });

            modelBuilder.Entity<Order>().HasOne(c => c.RequestPay).WithOne(c => c.Order).HasForeignKey<RequestPay>(c => c.OrderId);
            modelBuilder.Entity<PersonalWallet>().HasOne(c => c.User).WithOne(c => c.PersonalWallet).HasForeignKey<User>(c => c.PersonalWalletId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<CompanyWallet>().HasOne(c => c.User).WithOne(c => c.CompanyWallet).HasForeignKey<User>(c => c.CompanyWalletId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transaction>().HasOne(c => c.Wallet).WithMany(c => c.Transactions).HasForeignKey(c => c.WalletId).OnDelete(DeleteBehavior.NoAction);
            //--
            modelBuilder.Entity<User>().HasQueryFilter(c => !c.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(c => !c.IsRemoved);
            modelBuilder.Entity<CartItem>().HasQueryFilter(c => !c.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(c => !c.IsRemoved);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(c => !c.IsRemoved);
        }
    }
}
