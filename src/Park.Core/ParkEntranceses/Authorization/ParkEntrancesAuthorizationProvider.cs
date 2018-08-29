
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.ParkEntranceses.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="ParkEntrancesAppPermissions" /> for all permission names. ParkEntrances
    ///</summary>
    public class ParkEntrancesAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了ParkEntrances 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var parkentrances = administration.CreateChildPermission(ParkEntrancesAppPermissions.ParkEntrances , L("ParkEntranceses"));
parkentrances.CreateChildPermission(ParkEntrancesAppPermissions.ParkEntrances_Create, L("Create"));
parkentrances.CreateChildPermission(ParkEntrancesAppPermissions.ParkEntrances_Edit, L("Edit"));
parkentrances.CreateChildPermission(ParkEntrancesAppPermissions.ParkEntrances_Delete, L("Delete"));
parkentrances.CreateChildPermission(ParkEntrancesAppPermissions.ParkEntrances_BatchDelete , L("BatchDelete"));
parkentrances.CreateChildPermission(ParkEntrancesAppPermissions.ParkEntrances_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }