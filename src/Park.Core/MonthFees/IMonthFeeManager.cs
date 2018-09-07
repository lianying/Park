

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys.FareRules;


namespace Park.MonthFees
{
    public interface IMonthFeeManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitMonthFee();



		//// custom codes
 
        //// custom codes end

    }
}
