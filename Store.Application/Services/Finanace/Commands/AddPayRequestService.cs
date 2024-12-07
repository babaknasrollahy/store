using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Domain.Entities.Finance;

namespace Store.Application.Services.Finanace.Commands;

public class AddPayRequestService : IAddPayRequestService
{
    private readonly IStoreContext _db;
    public AddPayRequestService(IStoreContext db)
    {
        _db = db;
    }
    public ResultDTO<PayRequestResDTO> Execute(int UserId, int Amount)
    {
        var User = _db.Users.Find(UserId);
        var payR = new RequestPay()
        {
            Amount = Amount,
            User = User,
            UserId = UserId,
            Guid = Guid.NewGuid(),
            IsPayed = false,
        };
        _db.RequestPays.Add(payR);
        _db.SaveChanges();

        return new ResultDTO<PayRequestResDTO>()
        {
            Data = new PayRequestResDTO()
            {
                Guid = payR.Guid,
                Amount = Amount,
                Email = User.Email,
                Id = payR.Id,
            },
            isSuccess = true,
        };
    }
}