

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.CarUsers;


namespace Park.CarPorts
{
    public interface ICarPortManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitCarPort();



		//// custom codes
 
        //// custom codes end

    }
}
