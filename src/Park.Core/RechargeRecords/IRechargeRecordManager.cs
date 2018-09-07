

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.CarUsers;


namespace Park.RechargeRecords
{
    public interface IRechargeRecordManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitRechargeRecord();



		//// custom codes
 
        //// custom codes end

    }
}
