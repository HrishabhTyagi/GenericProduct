using Common.RequestModel;
using Common.ResponseModel;
using DAL;
using SqlServerEntity.EntityModel;
using System.Collections.Generic;

namespace BAL
{
    public class RoleBusiness : BaseBusiness
    {
        private readonly RoleDataAccess RoleDataAccess;
        public RoleBusiness()
        {
            RoleDataAccess = new RoleDataAccess();
        }

        public List<RoleResponse> GetRoleList()
        {
            List<Role> RoleList = RoleDataAccess.GetRoleList();
            List<RoleResponse> RoleListResponse = ListMapping<Role, RoleResponse>(RoleList);
            return RoleListResponse;
        }

        public RoleResponse GetRole(int RoleId)
        {
            Role Role = RoleDataAccess.GetRole(RoleId);
            RoleResponse RoleListResponse = ObjectMapping<Role, RoleResponse>(Role);
            return RoleListResponse;
        }

        public RoleResponse PostRole(RoleRequest RoleRequest)
        {
            Role Role = ObjectMapping<RoleRequest, Role>(RoleRequest);
            Role = RoleDataAccess.PostRole(Role);
            RoleResponse RoleResponse = ObjectMapping<Role, RoleResponse>(Role);
            return RoleResponse;
        }

        public int PutRole(RoleRequest RoleRequest)
        {
            Role Role = ObjectMapping<RoleRequest, Role>(RoleRequest);
            int result = RoleDataAccess.PutRole(Role);
            return result;
        }

        public int DeleteRole(int RoleId)
        {
            int result = RoleDataAccess.DeleteRole(RoleId);
            return result;
        }
    }
}
