
using System.Linq;
using Abp.Authorization;
using Abp.Localization;
using Park.Authorization;

namespace Park.RechargeRecords.Authorization
{
    /// <summary>
    /// 权限配置都在这里。
    /// 给权限默认设置服务
    /// See <see cref="RechargeRecordAppPermissions" /> for all permission names. RechargeRecord
    ///</summary>
    public class RechargeRecordAppAuthorizationProvider : AuthorizationProvider
    {
    public override void SetPermissions(IPermissionDefinitionContext context)
    {
    //在这里配置了RechargeRecord 的权限。
    var pages = context.GetPermissionOrNull(AppLtmPermissions.Pages) ?? context.CreatePermission(AppLtmPermissions.Pages, L("Pages"));

    var administration = pages.Children.FirstOrDefault(p => p.Name == AppLtmPermissions.Pages_Administration) ?? pages.CreateChildPermission(AppLtmPermissions.Pages_Administration, L("Administration"));

    var rechargerecord = administration.CreateChildPermission(RechargeRecordAppPermissions.RechargeRecord , L("RechargeRecords"));
rechargerecord.CreateChildPermission(RechargeRecordAppPermissions.RechargeRecord_Create, L("Create"));
rechargerecord.CreateChildPermission(RechargeRecordAppPermissions.RechargeRecord_Edit, L("Edit"));
rechargerecord.CreateChildPermission(RechargeRecordAppPermissions.RechargeRecord_Delete, L("Delete"));
rechargerecord.CreateChildPermission(RechargeRecordAppPermissions.RechargeRecord_BatchDelete , L("BatchDelete"));
rechargerecord.CreateChildPermission(RechargeRecordAppPermissions.RechargeRecord_ExportToExcel, L("ExportToExcel"));


    //// custom codes
    
    //// custom codes end
    }

    private static ILocalizableString L(string name)
    {
    return new LocalizableString(name, ParkConsts.LocalizationSourceName);
    }
    }
    }