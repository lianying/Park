

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.ParkAreas;


namespace Park.ParkAreases
{
    /// <summary>
    /// ParkAreas领域层的业务管理
    ///</summary>
    public class ParkAreasManager :ParkDomainServiceBase, IParkAreasManager
    {
    private readonly IRepository<ParkAreas,long> _parkareasRepository;

        /// <summary>
            /// ParkAreas的构造方法
            ///</summary>
        public ParkAreasManager(IRepository<ParkAreas, long>
parkareasRepository)
            {
            _parkareasRepository =  parkareasRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitParkAreas()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
