

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.CarUsers;


namespace Park.CarNumberses
{
    public interface ICarNumbersManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitCarNumbers();



		//// custom codes
 
        //// custom codes end

    }
}
