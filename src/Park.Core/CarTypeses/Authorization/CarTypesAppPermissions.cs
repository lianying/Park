
namespace Park.CarTypeses.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="CarTypesAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class CarTypesAppPermissions
    {
    /// <summary>
        /// CarTypes管理权限_自带查询授权
        ///</summary>
    public const string CarTypes = "Pages.CarTypes";

    /// <summary>
        /// CarTypes创建权限
        ///</summary>
    public const string CarTypes_Create = "Pages.CarTypes.Create";

    /// <summary>
        /// CarTypes修改权限
        ///</summary>
    public const string CarTypes_Edit = "Pages.CarTypes.Edit";

    /// <summary>
        /// CarTypes删除权限
        ///</summary>
    public const string CarTypes_Delete = "Pages.CarTypes.Delete";

    /// <summary>
        /// CarTypes批量删除权限
        ///</summary>
    public const string CarTypes_BatchDelete  = "Pages.CarTypes.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string CarTypes_ExportToExcel = "Pages.CarTypes.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

