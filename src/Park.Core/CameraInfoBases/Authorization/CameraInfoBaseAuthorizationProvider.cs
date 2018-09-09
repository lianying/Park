
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.CameraInfoBases.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="CameraInfoBaseAppPermissions" /> for all permission names. CameraInfoBase
    ///</summary>
    public class CameraInfoBaseAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了CameraInfoBase 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var camerainfobase = administration.CreateChildPermission(CameraInfoBaseAppPermissions.CameraInfoBase , L("CameraInfoBases"));
camerainfobase.CreateChildPermission(CameraInfoBaseAppPermissions.CameraInfoBase_Create, L("Create"));
camerainfobase.CreateChildPermission(CameraInfoBaseAppPermissions.CameraInfoBase_Edit, L("Edit"));
camerainfobase.CreateChildPermission(CameraInfoBaseAppPermissions.CameraInfoBase_Delete, L("Delete"));
camerainfobase.CreateChildPermission(CameraInfoBaseAppPermissions.CameraInfoBase_BatchDelete , L("BatchDelete"));
camerainfobase.CreateChildPermission(CameraInfoBaseAppPermissions.CameraInfoBase_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }