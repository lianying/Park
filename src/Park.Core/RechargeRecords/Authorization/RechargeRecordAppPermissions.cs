
namespace Park.RechargeRecords.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="RechargeRecordAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class RechargeRecordAppPermissions
    {
    /// <summary>
        /// RechargeRecord管理权限_自带查询授权
        ///</summary>
    public const string RechargeRecord = "Pages.RechargeRecord";

    /// <summary>
        /// RechargeRecord创建权限
        ///</summary>
    public const string RechargeRecord_Create = "Pages.RechargeRecord.Create";

    /// <summary>
        /// RechargeRecord修改权限
        ///</summary>
    public const string RechargeRecord_Edit = "Pages.RechargeRecord.Edit";

    /// <summary>
        /// RechargeRecord删除权限
        ///</summary>
    public const string RechargeRecord_Delete = "Pages.RechargeRecord.Delete";

    /// <summary>
        /// RechargeRecord批量删除权限
        ///</summary>
    public const string RechargeRecord_BatchDelete  = "Pages.RechargeRecord.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string RechargeRecord_ExportToExcel = "Pages.RechargeRecord.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

