

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.CarUsers;


namespace Park.RechargeRecords
{
    /// <summary>
    /// RechargeRecord领域层的业务管理
    ///</summary>
    public class RechargeRecordManager :ParkDomainServiceBase, IRechargeRecordManager
    {
    private readonly IRepository<RechargeRecord,long> _rechargerecordRepository;

        /// <summary>
            /// RechargeRecord的构造方法
            ///</summary>
        public RechargeRecordManager(IRepository<RechargeRecord, long>
rechargerecordRepository)
            {
            _rechargerecordRepository =  rechargerecordRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitRechargeRecord()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
