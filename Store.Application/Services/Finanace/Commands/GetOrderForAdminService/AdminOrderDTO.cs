using Store.Domain.Entities.Finance;

namespace Store.Application.Services.Finanace.Commands.GetOrderForAdminService
{
    public class AdminOrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RequestPayId { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }
        public OrderState OrderState { get; set; }
    }
}
