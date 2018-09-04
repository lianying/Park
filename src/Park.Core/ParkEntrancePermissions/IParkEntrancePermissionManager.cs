

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.ParkEntrances;


namespace Park.ParkEntrancePermissions
{
    public interface IParkEntrancePermissionManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitParkEntrancePermission();



		//// custom codes
 
        //// custom codes end

    }
}
