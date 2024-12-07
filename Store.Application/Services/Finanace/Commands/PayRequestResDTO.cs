namespace Store.Application.Services.Finanace.Commands;

public class PayRequestResDTO
{
    public int Id { get; set; }
    public int Amount { get; set; }
    public string Email { get; set; }
    public Guid Guid { get; set; }
}