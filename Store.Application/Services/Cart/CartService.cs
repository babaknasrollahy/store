using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly IStoreContext _db;
        public CartService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO AddToCart(int productId, Guid BrowserId, int? UserId = null)
        {
            var res = new ResultDTO()
            {
                isSuccess = true,
            };
            var cart = _db.Carts
                .Include(c => c.User)
                .Where(c => c.BrowserId == BrowserId && c.IsFinished == false).FirstOrDefault();
            if (cart == null)
            {
                cart = new Domain.Entities.Cart.Cart()
                {
                    BrowserId = BrowserId,
                    IsFinished = false,
                };
                _db.Carts.Add(cart);
            }
            if (UserId != null && cart.UserId == null)
            {
                cart.UserId = UserId.Value;
            }
            var cartItem = _db.CartItems
                .Include(c => c.Cart)
                .Include(c => c.Product).SingleOrDefault(c => c.ProductId == productId && c.Cart.Id == cart.Id);
            if (cartItem == null)
            {
                var p = _db.Products.Find(productId);
                cartItem = new Domain.Entities.Cart.CartItem()
                {
                    Cart = cart,
                    CartId = cart.Id,
                    Count = 1,
                    Price = p?.Price ?? throw new Exception("null!!!!!!!"),
                    Product = p,
                    ProductId = productId,
                };
                _db.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Count++;
            }
            _db.SaveChanges();
            return res;
        }
        public ResultDTO IncreaseCartItem(int itemId)
        {
            var c = _db.CartItems.Find(itemId);
            c.Count++;
            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
            };
        }
        public ResultDTO LowFromCart(int itemId)
        {
            var c = _db.CartItems.Find(itemId);
            if (c.Count > 1)
                c.Count--;

            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
            };
        }

        public ResultDTO DeleteFromCart(int ItemId)
        {
            var cartItem = _db.CartItems
                .Where(c => c.Id == ItemId).FirstOrDefault();
            cartItem.IsRemoved = true;

            _db.SaveChanges();
            return new ResultDTO()
            {
                isSuccess = true,
            };
        }
        public ResultDTO<CartDTO> GetCart(Guid BrowserId, int? UserId = null)
        {
            Domain.Entities.Cart.Cart cart = new Domain.Entities.Cart.Cart();
            var cartQuery = _db.Carts
                .Include(c => c.CartItems)
                .ThenInclude(c => c.Product)
                .ThenInclude(c => c.ProductImages)
                .Include(c => c.User)
                .AsQueryable();

            if (UserId != null)
                cart = cartQuery.Where(c => c.UserId == UserId && c.IsFinished == false).OrderByDescending(c => c.Id).FirstOrDefault();
            else
                cart = cartQuery.Where(c => c.BrowserId == BrowserId && c.IsFinished == false).OrderByDescending(c => c.Id).FirstOrDefault();

            if (cart == null)
            {
                cart = new Domain.Entities.Cart.Cart()
                {
                    BrowserId = BrowserId,
                    IsFinished = false,
                };
                _db.Carts.Add(cart);
                if (UserId != null)
                {
                    cart.UserId = UserId.Value;
                }
                _db.SaveChanges();
            }

            return new ResultDTO<CartDTO>()
            {
                isSuccess = true,
                Data = new CartDTO()
                {
                    Id = cart.Id,
                    ProductCount = cart?.CartItems?.Count() ?? 0,
                    TotalAmount = cart?.CartItems?.Sum(c => c.Price * c.Count) ?? 0,
                    CartItems = cart?.CartItems?.Select(c => new CartItemDTO()
                    {
                        CartId = cart.Id,
                        Count = c.Count,
                        Id = c.Id,
                        Price = c.Price,
                        Product = c.Product.Name,
                        ProductImg = c.Product.ProductImages.First().Src,
                    })?.ToList() ?? new List<CartItemDTO>(),
                }
            };
        }
    }
}
