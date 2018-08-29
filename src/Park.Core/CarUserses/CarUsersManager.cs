

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.CarUsers;


namespace Park.CarUserses
{
    /// <summary>
    /// CarUsers领域层的业务管理
    ///</summary>
    public class CarUsersManager :ParkDomainServiceBase, ICarUsersManager
    {
    private readonly IRepository<CarUsers,long> _carusersRepository;

        /// <summary>
            /// CarUsers的构造方法
            ///</summary>
        public CarUsersManager(IRepository<CarUsers, long>
carusersRepository)
            {
            _carusersRepository =  carusersRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitCarUsers()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
