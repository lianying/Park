using System.Collections.Generic;
using Park.Roles.Dto;
using Park.Users.Dto;

namespace Park.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}