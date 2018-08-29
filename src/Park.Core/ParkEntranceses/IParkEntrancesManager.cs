

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.ParkEntrances;


namespace Park.ParkEntranceses
{
    public interface IParkEntrancesManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitParkEntrances();



		//// custom codes
 
        //// custom codes end

    }
}
