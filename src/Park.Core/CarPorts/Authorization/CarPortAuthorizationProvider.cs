
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.CarPorts.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="CarPortAppPermissions" /> for all permission names. CarPort
    ///</summary>
    public class CarPortAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了CarPort 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var carport = administration.CreateChildPermission(CarPortAppPermissions.CarPort , L("CarPorts"));
carport.CreateChildPermission(CarPortAppPermissions.CarPort_Create, L("Create"));
carport.CreateChildPermission(CarPortAppPermissions.CarPort_Edit, L("Edit"));
carport.CreateChildPermission(CarPortAppPermissions.CarPort_Delete, L("Delete"));
carport.CreateChildPermission(CarPortAppPermissions.CarPort_BatchDelete , L("BatchDelete"));
carport.CreateChildPermission(CarPortAppPermissions.CarPort_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }