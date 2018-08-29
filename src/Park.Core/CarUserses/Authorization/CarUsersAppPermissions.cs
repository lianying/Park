
namespace Park.CarUserses.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="CarUsersAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class CarUsersAppPermissions
    {
    /// <summary>
        /// CarUsers管理权限_自带查询授权
        ///</summary>
    public const string CarUsers = "Pages.CarUsers";

    /// <summary>
        /// CarUsers创建权限
        ///</summary>
    public const string CarUsers_Create = "Pages.CarUsers.Create";

    /// <summary>
        /// CarUsers修改权限
        ///</summary>
    public const string CarUsers_Edit = "Pages.CarUsers.Edit";

    /// <summary>
        /// CarUsers删除权限
        ///</summary>
    public const string CarUsers_Delete = "Pages.CarUsers.Delete";

    /// <summary>
        /// CarUsers批量删除权限
        ///</summary>
    public const string CarUsers_BatchDelete  = "Pages.CarUsers.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string CarUsers_ExportToExcel = "Pages.CarUsers.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

