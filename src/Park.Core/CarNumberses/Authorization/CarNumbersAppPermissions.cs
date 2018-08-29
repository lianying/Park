
namespace Park.CarNumberses.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="CarNumbersAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class CarNumbersAppPermissions
    {
    /// <summary>
        /// CarNumbers管理权限_自带查询授权
        ///</summary>
    public const string CarNumbers = "Pages.CarNumbers";

    /// <summary>
        /// CarNumbers创建权限
        ///</summary>
    public const string CarNumbers_Create = "Pages.CarNumbers.Create";

    /// <summary>
        /// CarNumbers修改权限
        ///</summary>
    public const string CarNumbers_Edit = "Pages.CarNumbers.Edit";

    /// <summary>
        /// CarNumbers删除权限
        ///</summary>
    public const string CarNumbers_Delete = "Pages.CarNumbers.Delete";

    /// <summary>
        /// CarNumbers批量删除权限
        ///</summary>
    public const string CarNumbers_BatchDelete  = "Pages.CarNumbers.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string CarNumbers_ExportToExcel = "Pages.CarNumbers.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

