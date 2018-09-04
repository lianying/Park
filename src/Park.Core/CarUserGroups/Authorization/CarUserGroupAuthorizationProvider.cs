
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.CarUserGroups.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="CarUserGroupAppPermissions" /> for all permission names. CarUserGroup
    ///</summary>
    public class CarUserGroupAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了CarUserGroup 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var carusergroup = administration.CreateChildPermission(CarUserGroupAppPermissions.CarUserGroup , L("CarUserGroups"));
carusergroup.CreateChildPermission(CarUserGroupAppPermissions.CarUserGroup_Create, L("Create"));
carusergroup.CreateChildPermission(CarUserGroupAppPermissions.CarUserGroup_Edit, L("Edit"));
carusergroup.CreateChildPermission(CarUserGroupAppPermissions.CarUserGroup_Delete, L("Delete"));
carusergroup.CreateChildPermission(CarUserGroupAppPermissions.CarUserGroup_BatchDelete , L("BatchDelete"));
carusergroup.CreateChildPermission(CarUserGroupAppPermissions.CarUserGroup_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }