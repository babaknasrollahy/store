using Store.Application.Interfaces.Contexts;
using Store.Common;

namespace Store.Application.Services.User.Queries.GetRoles
{
    public class GetRolesService : IGetRolesService
    {
        private readonly IStoreContext _db;
        public GetRolesService(IStoreContext context)
        {
            _db = context;
        }
        public ResultDTO<List<GetRoleDTO>> Execute()
        {
            var Roles = _db.Roles.Select(c => new GetRoleDTO()
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();

            return new ResultDTO<List<GetRoleDTO>>()
            {
                Data = Roles,
                isSuccess = true,
                Message = "done!"
            };
        }
    }
}
