

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.CarTypes;


namespace Park.CarTypeses
{
    public interface ICarTypesManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitCarTypes();



		//// custom codes
 
        //// custom codes end

    }
}
