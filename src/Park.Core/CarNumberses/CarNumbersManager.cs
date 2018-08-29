

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.CarUsers;


namespace Park.CarNumberses
{
    /// <summary>
    /// CarNumbers领域层的业务管理
    ///</summary>
    public class CarNumbersManager :ParkDomainServiceBase, ICarNumbersManager
    {
    private readonly IRepository<CarNumbers,long> _carnumbersRepository;

        /// <summary>
            /// CarNumbers的构造方法
            ///</summary>
        public CarNumbersManager(IRepository<CarNumbers, long>
carnumbersRepository)
            {
            _carnumbersRepository =  carnumbersRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitCarNumbers()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
