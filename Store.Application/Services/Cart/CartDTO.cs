namespace Store.Application.Services.Cart
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int ProductCount { get; set; }
        public int TotalAmount { get; set; }
        public List<CartItemDTO> CartItems { get; set; }
    }
}
