using System.Threading.Tasks;
using Abp.Application.Services;
using Park.Sessions.Dto;

namespace Park.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<LoginResultDto> GetCurrentWebInfo();
    }
}
