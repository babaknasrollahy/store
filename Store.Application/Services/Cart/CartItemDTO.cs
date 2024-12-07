namespace Store.Application.Services.Cart
{
    public class CartItemDTO
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string ProductImg { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public int CartId { get; set; }
    }
}
