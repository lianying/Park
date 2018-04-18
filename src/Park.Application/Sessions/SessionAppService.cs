using System.Threading.Tasks;
using Abp.Auditing;
using Abp.AutoMapper;
using Park.Sessions.Dto;
using Abp.Authorization;
using Abp.Application.Navigation;
using Abp.Runtime.Session;

namespace Park.Sessions
{
    public class SessionAppService : ParkAppServiceBase, ISessionAppService
    {
        private readonly IUserNavigationManager _userNavigationManager;
        public SessionAppService(IUserNavigationManager userNavigationManager) {
            this._userNavigationManager = userNavigationManager;
        }
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput();

            if (AbpSession.UserId.HasValue)
            {
                output.User = (await GetCurrentUserAsync()).MapTo<UserLoginInfoDto>();
            }

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = (await GetCurrentTenantAsync()).MapTo<TenantLoginInfoDto>();
            }

            return output;
        }

        [AbpAuthorize]
        public async Task<LoginResultDto> GetCurrentWebInfo()
        {
            var output = new LoginResultDto();

           
            output.Menu = await _userNavigationManager.GetMenuAsync("MainMenu", AbpSession.ToUserIdentifier());

            output.App = new AppConsts() {  name=ParkConsts.AppName,  description=ParkConsts.Description , year=AppVersionHelper.ReleaseDate};

            


            return output;
        }
    }
}