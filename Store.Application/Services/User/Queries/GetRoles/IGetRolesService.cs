using Store.Common;

namespace Store.Application.Services.User.Queries.GetRoles
{
    public interface IGetRolesService
    {
        public ResultDTO<List<GetRoleDTO>> Execute();
    }
}
