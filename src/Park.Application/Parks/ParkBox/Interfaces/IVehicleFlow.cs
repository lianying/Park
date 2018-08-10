using Abp.Application.Services;
using Park.Entitys.Box;
using Park.Entitys.CarUsers;
using Park.Parks.Devices.Interfaces;
using Park.Parks.Entrance;
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



        //void CarOutRecord(string carNumber, EntranceDto entranceDto, IDeviceable deviceable, Action openRod, Action<CarOutRecord> setOutInfo, Action<string, string> showMessage);

        CarOutRecord CarOut(CarInRecord carIn, CarOutModel carOutModel);


        CarOutRecord CarOut(string CarNumber, CarUsers users, CarOutModel carOutModel);



        IsCarInModel IsCarIn(int parkId, string carNumber);


        /// <summary>
        /// 根据车牌号获取相似场内相似车牌
        /// </summary>
        /// <param name="parkId"></param>
        /// <param name="carNumber"></param>
        /// <returns></returns>
        List<CarInRecord> LevenshteinDistance(int parkId, string carNumber);



        CarDiscount GetCarDiscount(int parkId, string carNumber);

        CarErrorRecord CarErrorOut(CarInRecord carIn, CarOutModel carOutModel);
        
    }
}
