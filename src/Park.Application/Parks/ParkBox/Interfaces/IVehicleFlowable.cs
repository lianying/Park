﻿using Park.Entitys.Box;
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
    public interface IVehicleFlow
    {
        /// <summary>
        /// 入场流程
        /// </summary>

        bool CarIn(CarInModel carIn, PermissionResult permissionResult);



        bool CarOut(CarInRecord carIn, CarOutModel carOutModel);



        IsCarInModel IsCarIn(string carNumber);



    }
}
