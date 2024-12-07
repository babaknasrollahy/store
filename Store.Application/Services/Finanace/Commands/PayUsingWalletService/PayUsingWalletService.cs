using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Domain.Entities.Finance;

namespace Store.Application.Services.Finanace.Commands.PayUsingWalletService
{
    public class PayUsingWalletService : IPayUsingWalletService
    {
        private readonly IStoreContext _db;

        public PayUsingWalletService(IStoreContext db)
        {
            _db = db;
        }
        public async Task<ResultDTO> ExecuteAsync(int userId, bool useCompanyWallet)
        {
            int amount = 0;
            ResultDTO result = new ResultDTO() { isSuccess = false };
            try
            {
                var user = await _db.Users
                    .Include(c => c.CompanyWallet)
                    .ThenInclude(c => c.Transactions)
                    .Include(c => c.PersonalWallet)
                    .ThenInclude(c => c.Transactions)
                    .FirstOrDefaultAsync(c => c.Id == userId);

                var cart = await _db.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(c => c.Product)
                    .SingleOrDefaultAsync(c => c.UserId == userId && c.IsFinished == false);

                ArgumentNullException.ThrowIfNull(user);
                ArgumentNullException.ThrowIfNull(cart);

                amount = cart.CartItems.Sum(c => c.Price * c.Count);
                decimal balance = 0;
                if (useCompanyWallet)
                    balance = user.CompanyWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.InCome).Sum(c => c.Amount) - user.CompanyWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.OutCome).Sum(c => c.Amount);
                else
                    balance = user.PersonalWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.InCome).Sum(c => c.Amount) - user.PersonalWallet.Transactions.Where(c => c.TransactionType == Domain.Entities.Wallet.TransactionType.OutCome).Sum(c => c.Amount);


                if (amount > balance)
                    return new ResultDTO() { isSuccess = false, Message = "Amount is out of Balance!!" };

                var payRequest = new RequestPay()
                {
                    Guid = Guid.NewGuid(),
                    Amount = amount,
                    Authority = "",
                    IsPayed = true,
                    RefId = -1,
                    PayTime = DateTime.Now,
                    User = user,
                    UserId = user.Id,
                };

                var order = new Order()
                {
                    Address = "",
                    RequestPay = payRequest,
                    RequestPayId = payRequest.Id,
                    User = user,
                    UserId = user.Id,
                    OrderState = OrderState.Done,
                };
                payRequest.Order = order;
                payRequest.OrderId = order.Id;

                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (var item in cart.CartItems)
                {
                    var temp = new OrderDetail()
                    {
                        Count = item.Count,
                        Order = order,
                        OrderId = order.Id,
                        Product = item.Product,
                        ProductId = item.Product.Id,
                        Price = item.Product.Price,
                    };
                    orderDetails.Add(temp);
                }
                order.OrderDetails = orderDetails;
                cart.IsFinished = true;

                await _db.OrderDetails.AddRangeAsync(orderDetails);
                await _db.RequestPays.AddAsync(payRequest);
                await _db.Orders.AddAsync(order);
                await _db.SaveChangesAsync();

                result.isSuccess = true;
                result.Message = "Done!";
                return result;
            }
            catch
            {
                result.Message = "There was an error occurding create order and payRequest";
                return result;
            }
            finally
            {
                if (result.isSuccess)
                {
                    Domain.Entities.Wallet.Wallet? wallet;
                    if (useCompanyWallet)
                        wallet = await _db.CompanyWallets.FirstOrDefaultAsync(c => c.UserId == userId);
                    else
                        wallet = await _db.PersonalWallets.FirstOrDefaultAsync(c => c.UserId == userId);

                    if (wallet != null)
                    {
                        wallet.Transactions.Add(new Domain.Entities.Wallet.Transaction()
                        {
                            UserId = userId,
                            Amount = amount,
                            TransactionType = Domain.Entities.Wallet.TransactionType.OutCome,
                            WalletId = wallet.Id,
                        });
                        await _db.SaveChangesAsync();
                    }
                }
            }

        }
    }
}
