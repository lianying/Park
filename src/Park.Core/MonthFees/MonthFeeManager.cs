

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.FareRules;


namespace Park.MonthFees
{
    /// <summary>
    /// MonthFee领域层的业务管理
    ///</summary>
    public class MonthFeeManager :ParkDomainServiceBase, IMonthFeeManager
    {
    private readonly IRepository<MonthFee,int> _monthfeeRepository;

        /// <summary>
            /// MonthFee的构造方法
            ///</summary>
        public MonthFeeManager(IRepository<MonthFee,int >
monthfeeRepository)
            {
            _monthfeeRepository =  monthfeeRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitMonthFee()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
