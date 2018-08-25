using Abp.Application.Services.Dto;
using Park.Authorization.Roles;
using Park.Authorization.Users.Dto;
using Park.Dtos;
using System;
using System.Threading.Tasks;

namespace Park.Authorization.Users
{
    public class UserAppService : IUserAppService
    {

        private readonly RoleManager _roleManager;
        private readonly IUserEmailer userEmailer;


        public Task CreateOrUpdateUser(CreateOrUpdateUserInput input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserPermissionsForEditOutput> GetUserPermissionsForEdit(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<UserListDto>> GetUsers(GetUsersInput input)
        {
            throw new NotImplementedException();
        }

        public Task<FileDto> GetUsersToExcel()
        {
            throw new NotImplementedException();
        }

        public Task ResetUserSpecificPermissions(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }

        public Task UnlockUser(EntityDto<long> input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserPermissions(UpdateUserPermissionsInput input)
        {
            throw new NotImplementedException();
        }
    }
}
