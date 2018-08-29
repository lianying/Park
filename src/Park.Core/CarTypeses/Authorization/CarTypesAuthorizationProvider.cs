
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.CarTypeses.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="CarTypesAppPermissions" /> for all permission names. CarTypes
    ///</summary>
    public class CarTypesAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了CarTypes 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var cartypes = administration.CreateChildPermission(CarTypesAppPermissions.CarTypes , L("CarTypeses"));
cartypes.CreateChildPermission(CarTypesAppPermissions.CarTypes_Create, L("Create"));
cartypes.CreateChildPermission(CarTypesAppPermissions.CarTypes_Edit, L("Edit"));
cartypes.CreateChildPermission(CarTypesAppPermissions.CarTypes_Delete, L("Delete"));
cartypes.CreateChildPermission(CarTypesAppPermissions.CarTypes_BatchDelete , L("BatchDelete"));
cartypes.CreateChildPermission(CarTypesAppPermissions.CarTypes_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }