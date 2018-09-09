

using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Park;
using Park.Entitys;


namespace Park.CameraInfoBases
{
    /// <summary>
    /// CameraInfoBase领域层的业务管理
    ///</summary>
    public class CameraInfoBaseManager :ParkDomainServiceBase, ICameraInfoBaseManager
    {
    private readonly IRepository<CameraInfoBase,int> _camerainfobaseRepository;

        /// <summary>
            /// CameraInfoBase的构造方法
            ///</summary>
        public CameraInfoBaseManager(IRepository<CameraInfoBase, int>
camerainfobaseRepository)
            {
            _camerainfobaseRepository =  camerainfobaseRepository;
            }


            /// <summary>
                ///     初始化
                ///</summary>
            public void InitCameraInfoBase()
            {
            throw new NotImplementedException();
            }

            //TODO:编写领域业务代码



            //// custom codes
             
            //// custom codes end

            }
            }
