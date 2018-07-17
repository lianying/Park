using Park.Devices.Models;
using Park.ParkBox;
using Park.Parks.ParkBox.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Models;
using Castle.Core.Logging;

namespace Park.Parks.ParkBox.Core
{
    public class BoxMessage : IBoxMessage
    {
        private readonly IParkBoxOptions parkBoxOptions;

        private readonly IVehicleFlow vehicleFlowable;


        private readonly ICarNumberPermission carNumberPermission;

        private readonly INotifyConfim notifyConfim;
        private readonly LedManager ledManager;

        public ILogger Logger { get; set; }


        public  BoxMessage(IParkBoxOptions parkBoxOptions,
            IVehicleFlow vehicleFlowable,
            ICarNumberPermission carNumberPermission, 
            INotifyConfim notifyConfim,
            LedManager ledManager)
        {

            this.parkBoxOptions = parkBoxOptions;

            this.vehicleFlowable = vehicleFlowable;

            this.carNumberPermission = carNumberPermission;

            this.notifyConfim = notifyConfim;
            this.ledManager = ledManager;


        }

        public async void DoMessage(DeviceInfoDto deviceInfoDto)
        {
            DateTime photoTime = DateTime.Now;
            var result = deviceInfoDto.Controlable.Camerable.GetPlateNumber();
            
            if (deviceInfoDto.EntranceDto.EntranceType == Enum.EntranceType.In)
            {
                //禁止非机动车入场
                if (!parkBoxOptions.NonmotorVehicleIn && result.Item4 == CarTypeEnum.NonMotorVehicle)
                {
                    Logger.Info("非机动车辆禁止进入" + result.Item1);
                    return;
                }
                var permission = carNumberPermission.CheckCarNumberPermission(result.Item1);

                if (permission.IsCarIn.HasValue && !permission.IsCarIn.Value)
                {
                    ///调用Led显示屏展示内容
                    ////*
                    /// 
                    /// */
                    Logger.Info("禁止入场  " + permission.ToString());
                    return;
                }

                if (!permission.IsCarIn.HasValue) {
                    if (!notifyConfim.Confirm("提示", permission.Message))
                    {
                        Logger.Info("拒绝入场" + permission.ToString());
                        return;
                    }
                }
                var carInModel = new CarInModel()
                {
                    CarNumber = result.Item1,
                    InOutType = Enum.InOutTypeEnum.Photo,
                    InPhotoTime = photoTime,
                    Img = result.Item2,
                    InTime = DateTime.Now,
                    Entrance = deviceInfoDto.EntranceDto
                };
                ///入场成功
                if (vehicleFlowable.CarIn(carInModel
                   , permission))
                {
                   await deviceInfoDto.Controlable.Camerable.OpenRod(); //抬杆
                    await ledManager.SpeakAndShowText(deviceInfoDto, carInModel, permission); //播报语音
                }
                else
                {
                    notifyConfim.Message("提示", "入场失败");
                }

            }
            else
            {
                /*
                 出场逻辑中 无场内车辆的记录也弹出    
                */
                var isCarIn = vehicleFlowable.IsCarIn(deviceInfoDto.EntranceDto.ParkLevel.Park.Id, result.Item1);
                if (isCarIn.IsCarIn)  //在场内
                {
                    /*
                    算费逻辑在此处执行  0元直接放行时避免收费弹窗一闪而过    
                 */

                    
                    
                }
                else {

                }
                
            }
        }
    }
}
