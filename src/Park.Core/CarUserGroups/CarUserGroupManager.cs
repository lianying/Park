

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.CarUsers;


namespace Park.CarUserGroups
{
    /// <summary>
    /// CarUserGroup领域层的业务管理
    ///</summary>
    public class CarUserGroupManager :ParkDomainServiceBase, ICarUserGroupManager
    {
    private readonly IRepository<CarUserGroup,int> _carusergroupRepository;

        /// <summary>
            /// CarUserGroup的构造方法
            ///</summary>
        public CarUserGroupManager(IRepository<CarUserGroup, int>
carusergroupRepository)
            {
            _carusergroupRepository =  carusergroupRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitCarUserGroup()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
