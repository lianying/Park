

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.CarTypes;


namespace Park.CarTypeses
{
    /// <summary>
    /// CarTypes领域层的业务管理
    ///</summary>
    public class CarTypesManager :ParkDomainServiceBase, ICarTypesManager
    {
    private readonly IRepository<CarTypes,long> _cartypesRepository;

        /// <summary>
            /// CarTypes的构造方法
            ///</summary>
        public CarTypesManager(IRepository<CarTypes, long>
cartypesRepository)
            {
            _cartypesRepository =  cartypesRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitCarTypes()
            {
            //throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
