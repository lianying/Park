

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.CarUsers;


namespace Park.CarUserses
{
    public interface ICarUsersManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitCarUsers();



		//// custom codes
 
        //// custom codes end

    }
}
