using Park.Devices.Models;
using Park.ParkBox;
using Park.ParkBox.Dto;
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
                    new DeviceInfoDto(){  Ip="1.1.1.1", DeviceType= Enum.DeviceType.Camera, Password="123", UserName="123",
                        EquipmentManufacturers = Enum.EquipmentManufacturers.HaiKang, EntranceDto=new Parks.Entrance.EntranceDto(){ EntranceName="入口", EntranceType= Enum.EntranceType.In, Id=1, ParkLevel =new Level.Dto.ParkLevelDto(){  Id=1, LevelName="1", LevelNumber=1,
                            Park =new ParkDto(){  Id=4, Name="测试", Address="测试", CarportCount=100} } }  },
                    new DeviceInfoDto(){  Ip="1.1.1.2", DeviceType= Enum.DeviceType.Camera, Password="123", UserName="123",
                        EquipmentManufacturers = Enum.EquipmentManufacturers.HaiKang, EntranceDto=new Parks.Entrance.EntranceDto(){ EntranceName="出口", EntranceType= Enum.EntranceType.Out, Id=2 ,
                        ParkLevel =new Level.Dto.ParkLevelDto(){  Id=1, LevelName="1", LevelNumber=1,
                            Park =new ParkDto(){  Id=4, Name="测试", Address="测试", CarportCount=100} } } },

                    new DeviceInfoDto(){  Ip="1.1.1.3", DeviceType= Enum.DeviceType.Camera, Password="123", UserName="123",
                        EquipmentManufacturers = Enum.EquipmentManufacturers.HaiKang, EntranceDto=new Parks.Entrance.EntranceDto(){ EntranceName="入口", EntranceType= Enum.EntranceType.In, Id=3, ParkLevel =new Level.Dto.ParkLevelDto(){  Id=1, LevelName="1", LevelNumber=1,
                            Park =new ParkDto(){  Id=4, Name="测试", Address="测试", CarportCount=100} } }  },
                    new DeviceInfoDto(){  Ip="1.1.1.4", DeviceType= Enum.DeviceType.Camera, Password="123", UserName="123",
                        EquipmentManufacturers = Enum.EquipmentManufacturers.HaiKang, EntranceDto=new Parks.Entrance.EntranceDto(){ EntranceName="出口", EntranceType= Enum.EntranceType.Out, Id=4 ,
                        ParkLevel =new Level.Dto.ParkLevelDto(){  Id=1, LevelName="1", LevelNumber=1,
                            Park =new ParkDto(){  Id=4, Name="测试", Address="测试", CarportCount=100} } } },


                    new DeviceInfoDto(){  Ip="1.1.1.4", DeviceType= Enum.DeviceType.Camera, Password="123", UserName="123",
                        EquipmentManufacturers = Enum.EquipmentManufacturers.HaiKang, EntranceDto=new Parks.Entrance.EntranceDto(){ EntranceName="出口", EntranceType= Enum.EntranceType.Out, Id=5 ,
                        ParkLevel =new Level.Dto.ParkLevelDto(){  Id=1, LevelName="1", LevelNumber=1,
                            Park =new ParkDto(){  Id=4, Name="测试", Address="测试", CarportCount=100} } } }
            }; 

            parkBoxOptions.DeciceInfos = deviceInfos;

            parkBoxOptions.IsListView = false;

            parkBoxOptions.ParkName = "测试停车场";

            parkBoxOptions.ParkId = 4;

            
        }
    }
}
