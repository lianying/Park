

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys.ParkEntrances;


namespace Park.ParkEntrancePermissions
{
    /// <summary>
    /// ParkEntrancePermission领域层的业务管理
    ///</summary>
    public class ParkEntrancePermissionManager :ParkDomainServiceBase, IParkEntrancePermissionManager
    {
    private readonly IRepository<ParkEntrancePermission,long> _parkentrancepermissionRepository;

        /// <summary>
            /// ParkEntrancePermission的构造方法
            ///</summary>
        public ParkEntrancePermissionManager(IRepository<ParkEntrancePermission, long>
parkentrancepermissionRepository)
            {
            _parkentrancepermissionRepository =  parkentrancepermissionRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitParkEntrancePermission()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
