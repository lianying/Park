
namespace Park.ParkEntrancePermissions.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="ParkEntrancePermissionAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class ParkEntrancePermissionAppPermissions
    {
    /// <summary>
        /// ParkEntrancePermission管理权限_自带查询授权
        ///</summary>
    public const string ParkEntrancePermission = "Pages.ParkEntrancePermission";

    /// <summary>
        /// ParkEntrancePermission创建权限
        ///</summary>
    public const string ParkEntrancePermission_Create = "Pages.ParkEntrancePermission.Create";

    /// <summary>
        /// ParkEntrancePermission修改权限
        ///</summary>
    public const string ParkEntrancePermission_Edit = "Pages.ParkEntrancePermission.Edit";

    /// <summary>
        /// ParkEntrancePermission删除权限
        ///</summary>
    public const string ParkEntrancePermission_Delete = "Pages.ParkEntrancePermission.Delete";

    /// <summary>
        /// ParkEntrancePermission批量删除权限
        ///</summary>
    public const string ParkEntrancePermission_BatchDelete  = "Pages.ParkEntrancePermission.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string ParkEntrancePermission_ExportToExcel = "Pages.ParkEntrancePermission.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

