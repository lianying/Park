

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.CarUsers;


namespace Park.CarPorts
{
    /// <summary>
    /// CarPort领域层的业务管理
    ///</summary>
    public class CarPortManager :ParkDomainServiceBase, ICarPortManager
    {
    private readonly IRepository<CarPort,long> _carportRepository;

        /// <summary>
            /// CarPort的构造方法
            ///</summary>
        public CarPortManager(IRepository<CarPort, long>
carportRepository)
            {
            _carportRepository =  carportRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitCarPort()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
