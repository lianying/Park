

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.CarUsers;


namespace Park.CarUserGroups
{
    public interface ICarUserGroupManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitCarUserGroup();



		//// custom codes
 
        //// custom codes end

    }
}
