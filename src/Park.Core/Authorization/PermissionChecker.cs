using Abp.Authorization;
using Park.Authorization.Roles;
using Park.Authorization.Users;

namespace Park.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
