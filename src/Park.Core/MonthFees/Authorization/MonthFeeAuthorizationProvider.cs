
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.MonthFees.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="MonthFeeAppPermissions" /> for all permission names. MonthFee
    ///</summary>
    public class MonthFeeAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了MonthFee 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var monthfee = administration.CreateChildPermission(MonthFeeAppPermissions.MonthFee , L("MonthFees"));
monthfee.CreateChildPermission(MonthFeeAppPermissions.MonthFee_Create, L("Create"));
monthfee.CreateChildPermission(MonthFeeAppPermissions.MonthFee_Edit, L("Edit"));
monthfee.CreateChildPermission(MonthFeeAppPermissions.MonthFee_Delete, L("Delete"));
monthfee.CreateChildPermission(MonthFeeAppPermissions.MonthFee_BatchDelete , L("BatchDelete"));
monthfee.CreateChildPermission(MonthFeeAppPermissions.MonthFee_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }