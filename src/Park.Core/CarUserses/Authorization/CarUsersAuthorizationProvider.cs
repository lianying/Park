
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.CarUserses.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="CarUsersAppPermissions" /> for all permission names. CarUsers
    ///</summary>
    public class CarUsersAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了CarUsers 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var carusers = administration.CreateChildPermission(CarUsersAppPermissions.CarUsers , L("CarUserses"));
carusers.CreateChildPermission(CarUsersAppPermissions.CarUsers_Create, L("Create"));
carusers.CreateChildPermission(CarUsersAppPermissions.CarUsers_Edit, L("Edit"));
carusers.CreateChildPermission(CarUsersAppPermissions.CarUsers_Delete, L("Delete"));
carusers.CreateChildPermission(CarUsersAppPermissions.CarUsers_BatchDelete , L("BatchDelete"));
carusers.CreateChildPermission(CarUsersAppPermissions.CarUsers_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }