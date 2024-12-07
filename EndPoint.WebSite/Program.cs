using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPattern;
using Store.Application.Services.Cart;
using Store.Application.Services.Common.Queries.GetCategoriesSearchService;
using Store.Application.Services.Common.Queries.GetCategoriesUserService;
using Store.Application.Services.Finanace.Commands;
using Store.Application.Services.Finanace.Commands.GetOrderForAdminService;
using Store.Application.Services.Finanace.Commands.GetOrderService;
using Store.Application.Services.Finanace.Commands.PayUsingWalletService;
using Store.Application.Services.Finanace.Queries;
using Store.Application.Services.HomePage.HomePageFacadPattern;
using Store.Application.Services.Products.ProductFacadPattern;
using Store.Application.Services.User.UserFacadPattern;
using Store.Application.Services.Wallet.Shared.Queries.GetWalletByIdService;
using Store.Application.Services.Wallet.Shared.Queries.GetWalletsBalanceByUserService;
using Store.Application.Services.Wallet.Transaction.Commands.CreateTransactionService;
using Store.Common;
using Store.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);
//Get StoreDb Connction String Form AppSettings
string ConnectionString = builder.Configuration.GetConnectionString("StoreDB");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);

builder.Services.AddAuthorization(o => o.AddPolicy(nameof(RolesList.Customer), policy => policy.RequireRole
(
    nameof(RolesList.Customer),
    nameof(RolesList.Admin),
    nameof(RolesList.Operator)
    )
));
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/authentication/signin");
    options.AccessDeniedPath = new PathString("/NotFound");
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
});

builder.Services.AddScoped<IStoreContext, StoreContext>();
builder.Services.AddScoped<IUserFacadPattern, UserFacadPattern>();
builder.Services.AddScoped<IProductFacadPattern, ProductFacadPattern>();
builder.Services.AddScoped<IHomePageFacadPattern, HomePageFacadPattern>();

builder.Services.AddScoped<IGetCategoriesUserService, GetCategoriesUserService>();
builder.Services.AddScoped<IGetCategoriesSearchService, GetCategoriesSearchService>();

builder.Services.AddScoped<IAddPayRequestService, AddPayRequestService>();
builder.Services.AddScoped<IGetPayRequestService, GetPayRequestService>();

builder.Services.AddScoped<IGetOrderForAdminService, GetOrderForAdminService>();
builder.Services.AddScoped<IAddOrderService, AddOrderService>();
builder.Services.AddScoped<IGetOrderService, GetOrderService>();

builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<ICreateTransactionService, CreateTransactionService>();
builder.Services.AddScoped<IGetWalletByIdService, GetWalletByIdService>();
builder.Services.AddScoped<IGetWalletsBalanceByUserService, GetWalletsBalanceByUserService>();
builder.Services.AddScoped<IPayUsingWalletService, PayUsingWalletService>();

builder.Services.AddEntityFrameworkSqlServer().AddDbContext<StoreContext>(options => options.UseSqlServer(ConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
