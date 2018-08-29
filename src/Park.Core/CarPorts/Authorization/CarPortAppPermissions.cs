
namespace Park.CarPorts.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="CarPortAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class CarPortAppPermissions
    {
    /// <summary>
        /// CarPort管理权限_自带查询授权
        ///</summary>
    public const string CarPort = "Pages.CarPort";

    /// <summary>
        /// CarPort创建权限
        ///</summary>
    public const string CarPort_Create = "Pages.CarPort.Create";

    /// <summary>
        /// CarPort修改权限
        ///</summary>
    public const string CarPort_Edit = "Pages.CarPort.Edit";

    /// <summary>
        /// CarPort删除权限
        ///</summary>
    public const string CarPort_Delete = "Pages.CarPort.Delete";

    /// <summary>
        /// CarPort批量删除权限
        ///</summary>
    public const string CarPort_BatchDelete  = "Pages.CarPort.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string CarPort_ExportToExcel = "Pages.CarPort.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

