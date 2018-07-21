using Abp.Application.Services;
using Park.Entitys.Box;
using Park.Entitys.CarUsers;
using Park.Parks.ParkBox.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.ParkBox.Interfaces
{
    /// <summary>
    /// 车辆流程接口
    /// </summary>
    public interface IVehicleFlow: IApplicationService
    {
        /// <summary>
        /// 入场流程
        /// </summary>

        CarInRecord CarIn(CarInModel carIn, PermissionResult permissionResult);



        CarOutRecord CarOut(CarInRecord carIn, CarOutModel carOutModel);



        IsCarInModel IsCarIn(int parkId, string carNumber);



    }
}
