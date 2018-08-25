
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.ParkAreases.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="ParkAreasAppPermissions" /> for all permission names. ParkAreas
    ///</summary>
    public class ParkAreasAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了ParkAreas 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var parkareas = administration.CreateChildPermission(ParkAreasAppPermissions.ParkAreas , L("ParkAreases"));
parkareas.CreateChildPermission(ParkAreasAppPermissions.ParkAreas_Create, L("Create"));
parkareas.CreateChildPermission(ParkAreasAppPermissions.ParkAreas_Edit, L("Edit"));
parkareas.CreateChildPermission(ParkAreasAppPermissions.ParkAreas_Delete, L("Delete"));
parkareas.CreateChildPermission(ParkAreasAppPermissions.ParkAreas_BatchDelete , L("BatchDelete"));
parkareas.CreateChildPermission(ParkAreasAppPermissions.ParkAreas_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }