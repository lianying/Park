
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.ParkEntrancePermissions.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="ParkEntrancePermissionAppPermissions" /> for all permission names. ParkEntrancePermission
    ///</summary>
    public class ParkEntrancePermissionAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了ParkEntrancePermission 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var parkentrancepermission = administration.CreateChildPermission(ParkEntrancePermissionAppPermissions.ParkEntrancePermission , L("ParkEntrancePermissions"));
parkentrancepermission.CreateChildPermission(ParkEntrancePermissionAppPermissions.ParkEntrancePermission_Create, L("Create"));
parkentrancepermission.CreateChildPermission(ParkEntrancePermissionAppPermissions.ParkEntrancePermission_Edit, L("Edit"));
parkentrancepermission.CreateChildPermission(ParkEntrancePermissionAppPermissions.ParkEntrancePermission_Delete, L("Delete"));
parkentrancepermission.CreateChildPermission(ParkEntrancePermissionAppPermissions.ParkEntrancePermission_BatchDelete , L("BatchDelete"));
parkentrancepermission.CreateChildPermission(ParkEntrancePermissionAppPermissions.ParkEntrancePermission_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }