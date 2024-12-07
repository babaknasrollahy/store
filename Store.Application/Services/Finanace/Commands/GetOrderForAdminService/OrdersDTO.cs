namespace Store.Application.Services.Finanace.Commands.GetOrderForAdminService;

public class OrdersDTO
{
    public List<AdminOrderDTO> Orders { get; set; }
    public int Row { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}