
namespace Park.ParkAreases.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="ParkAreasAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class ParkAreasAppPermissions
    {
    /// <summary>
        /// ParkAreas管理权限_自带查询授权
        ///</summary>
    public const string ParkAreas = "Pages.ParkAreas";

    /// <summary>
        /// ParkAreas创建权限
        ///</summary>
    public const string ParkAreas_Create = "Pages.ParkAreas.Create";

    /// <summary>
        /// ParkAreas修改权限
        ///</summary>
    public const string ParkAreas_Edit = "Pages.ParkAreas.Edit";

    /// <summary>
        /// ParkAreas删除权限
        ///</summary>
    public const string ParkAreas_Delete = "Pages.ParkAreas.Delete";

    /// <summary>
        /// ParkAreas批量删除权限
        ///</summary>
    public const string ParkAreas_BatchDelete  = "Pages.ParkAreas.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string ParkAreas_ExportToExcel = "Pages.ParkAreas.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

