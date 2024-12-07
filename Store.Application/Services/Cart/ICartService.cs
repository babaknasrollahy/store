using Store.Common;

namespace Store.Application.Services.Cart;

public interface ICartService
{
    ResultDTO AddToCart(int productId, Guid BrowserId, int? UserId = null);
    ResultDTO DeleteFromCart(int ItemId);
    ResultDTO<CartDTO> GetCart(Guid BrowserId, int? UserId = null);
    ResultDTO IncreaseCartItem(int CartId);
    ResultDTO LowFromCart(int CartId);
}