
namespace Park.ParkEntranceses.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="ParkEntrancesAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class ParkEntrancesAppPermissions
    {
    /// <summary>
        /// ParkEntrances管理权限_自带查询授权
        ///</summary>
    public const string ParkEntrances = "Pages.ParkEntrances";

    /// <summary>
        /// ParkEntrances创建权限
        ///</summary>
    public const string ParkEntrances_Create = "Pages.ParkEntrances.Create";

    /// <summary>
        /// ParkEntrances修改权限
        ///</summary>
    public const string ParkEntrances_Edit = "Pages.ParkEntrances.Edit";

    /// <summary>
        /// ParkEntrances删除权限
        ///</summary>
    public const string ParkEntrances_Delete = "Pages.ParkEntrances.Delete";

    /// <summary>
        /// ParkEntrances批量删除权限
        ///</summary>
    public const string ParkEntrances_BatchDelete  = "Pages.ParkEntrances.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string ParkEntrances_ExportToExcel = "Pages.ParkEntrances.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

