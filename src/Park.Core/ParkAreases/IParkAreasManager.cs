

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.ParkAreas;


namespace Park.ParkAreases
{
    public interface IParkAreasManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitParkAreas();



		//// custom codes
 
        //// custom codes end

    }
}
