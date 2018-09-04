
namespace Park.CarUserGroups.Authorization
{

	 /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="CarUserGroupAppAuthorizationProvider" />中对权限的定义.
    ///</summary>
	public static  class CarUserGroupAppPermissions
    {
    /// <summary>
        /// CarUserGroup管理权限_自带查询授权
        ///</summary>
    public const string CarUserGroup = "Pages.CarUserGroup";

    /// <summary>
        /// CarUserGroup创建权限
        ///</summary>
    public const string CarUserGroup_Create = "Pages.CarUserGroup.Create";

    /// <summary>
        /// CarUserGroup修改权限
        ///</summary>
    public const string CarUserGroup_Edit = "Pages.CarUserGroup.Edit";

    /// <summary>
        /// CarUserGroup删除权限
        ///</summary>
    public const string CarUserGroup_Delete = "Pages.CarUserGroup.Delete";

    /// <summary>
        /// CarUserGroup批量删除权限
        ///</summary>
    public const string CarUserGroup_BatchDelete  = "Pages.CarUserGroup.BatchDelete";

	  /// <summary>
        /// 导出为Excel表
        ///</summary>
    public const string CarUserGroup_ExportToExcel = "Pages.CarUserGroup.ExportToExcel";


    //// custom codes
    
    //// custom codes end
    }

}

