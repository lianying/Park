using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Park.MultiTenancy.Dto;

namespace Park.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
