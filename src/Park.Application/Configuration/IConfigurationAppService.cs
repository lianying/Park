using System.Threading.Tasks;
using Abp.Application.Services;
using Park.Configuration.Dto;

namespace Park.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}