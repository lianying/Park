
namespace Park.MonthFees.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="MonthFeeAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class MonthFeeAppPermissions
    {
    /// <summary>
        /// MonthFee管理权限_自带查询授权
        ///</summary>
    public const string MonthFee = "Pages.MonthFee";

    /// <summary>
        /// MonthFee创建权限
        ///</summary>
    public const string MonthFee_Create = "Pages.MonthFee.Create";

    /// <summary>
        /// MonthFee修改权限
        ///</summary>
    public const string MonthFee_Edit = "Pages.MonthFee.Edit";

    /// <summary>
        /// MonthFee删除权限
        ///</summary>
    public const string MonthFee_Delete = "Pages.MonthFee.Delete";

    /// <summary>
        /// MonthFee批量删除权限
        ///</summary>
    public const string MonthFee_BatchDelete  = "Pages.MonthFee.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string MonthFee_ExportToExcel = "Pages.MonthFee.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

