using Store.Domain.Entities.Finance;

namespace Store.Application.Services.Finanace.Commands.GetOrderService
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int RequestPayId { get; set; }
        public string Address { get; set; }
        public OrderState OrderState { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
