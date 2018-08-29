
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.CarNumberses.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="CarNumbersAppPermissions" /> for all permission names. CarNumbers
    ///</summary>
    public class CarNumbersAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了CarNumbers 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var carnumbers = administration.CreateChildPermission(CarNumbersAppPermissions.CarNumbers , L("CarNumberses"));
carnumbers.CreateChildPermission(CarNumbersAppPermissions.CarNumbers_Create, L("Create"));
carnumbers.CreateChildPermission(CarNumbersAppPermissions.CarNumbers_Edit, L("Edit"));
carnumbers.CreateChildPermission(CarNumbersAppPermissions.CarNumbers_Delete, L("Delete"));
carnumbers.CreateChildPermission(CarNumbersAppPermissions.CarNumbers_BatchDelete , L("BatchDelete"));
carnumbers.CreateChildPermission(CarNumbersAppPermissions.CarNumbers_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }