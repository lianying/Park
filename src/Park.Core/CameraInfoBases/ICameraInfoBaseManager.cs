

using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Services;
using Park.Entitys;


namespace Park.CameraInfoBases
{
    public interface ICameraInfoBaseManager : IDomainService
    {

        /// <summary>
    /// 初始化方法
    ///</summary>
        void InitCameraInfoBase();



		//// custom codes
 
        //// custom codes end

    }
}
