using System.Collections.Generic;
using Shared.Resources.Permission;
using Shared.Resources.Role;
using Shared.Resources.UserType;

namespace Shared.Resources.User
{
    public class UserGetData
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsEditable { get; set; }
        public int Status { get; set; }
        public UserTypeGetData UserType { get; set; }
        public UserDetailGetData Detail { get; set; }
        public List<RoleGetData> Roles { get; set; }
        public List<PermissionGetData> DirectivePermissions { get; set; }

        public UserGetData()
        {
            Roles = new List<RoleGetData>();
            DirectivePermissions = new List<PermissionGetData>();
        }
    }
}
