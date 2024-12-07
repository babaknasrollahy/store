namespace Store.Application.Services.Finanace.Commands.GetOrderService
{
    public class OrderDetailDTO
    {
        public string ProductName { get; set; }
        public string ProductImg { get; set; }

        public int Count { get; set; }
        public int Price { get; set; }
    }
}
