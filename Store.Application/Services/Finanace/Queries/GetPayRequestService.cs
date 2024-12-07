using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.Finanace.Queries
{
    public class GetPayRequestService : IGetPayRequestService
    {
        private readonly IStoreContext _db;
        public GetPayRequestService(IStoreContext db)
        {
            _db = db;
        }
        public ResultDTO<PayRequestDTO> Execute(Guid guid)
        {
            var Request = _db.RequestPays.SingleOrDefault(c => c.Guid == guid);
            if (Request != null)
            {
                return new ResultDTO<PayRequestDTO>()
                {
                    Data = new PayRequestDTO()
                    {
                        Amount = Request.Amount,
                        Id = Request.Id,
                    },
                    isSuccess = true
                };
            }
            return new ResultDTO<PayRequestDTO>() { isSuccess = false };
        }
    }
}
