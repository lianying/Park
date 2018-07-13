using Park.Devices.Models;
using Park.ParkBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.ParkBox
{
    public static class MockParkOptions
    {
        public static void MockDevices(this IParkBoxOptions parkBoxOptions)
        {
            List<DeviceInfoDto> deviceInfos = new List<DeviceInfoDto>() {
                    new DeviceInfoDto(){  Ip="1.1.1.1", DeviceType= Enum.DeviceType.Camera, Password="123", UserName="123", EquipmentManufacturers= Enum.EquipmentManufacturers.HaiKang, EntranceDto=new Parks.Entrance.EntranceDto(){ EntranceName="入口", EntranceType= Enum.EntranceType.In, Id=1 } },
                    new DeviceInfoDto(){  Ip="1.1.1.1", DeviceType= Enum.DeviceType.Camera, Password="123", UserName="123", EquipmentManufacturers= Enum.EquipmentManufacturers.HaiKang, EntranceDto=new Parks.Entrance.EntranceDto(){ EntranceName="出口", EntranceType= Enum.EntranceType.In, Id=1 } }
            };

            parkBoxOptions.DeciceInfos = deviceInfos;

        }
    }
}
