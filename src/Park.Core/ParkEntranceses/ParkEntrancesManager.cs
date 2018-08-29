

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.ParkEntrances;


namespace Park.ParkEntranceses
{
    /// <summary>
    /// ParkEntrances领域层的业务管理
    ///</summary>
    public class ParkEntrancesManager :ParkDomainServiceBase, IParkEntrancesManager
    {
    private readonly IRepository<ParkEntrances,long> _parkentrancesRepository;

        /// <summary>
            /// ParkEntrances的构造方法
            ///</summary>
        public ParkEntrancesManager(IRepository<ParkEntrances, long>
parkentrancesRepository)
            {
            _parkentrancesRepository =  parkentrancesRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitParkEntrances()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
