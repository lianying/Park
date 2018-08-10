using Abp.AutoMapper;
using Park.Entitys.Cameras;
using Park.Enum;
using Park.Parks.Entrance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Park.Parks.Devices.Interfaces;
using System.IO;
using Park.Entitys;
using Park.SDK;
using Park.DeviceSDK;
using Park.DeviceSDK.LanKa;

namespace Park.Devices.Models
{
    [AutoMap(typeof(Device))]
    public  class DeviceInfoDto: CameraInfoBase,IDeviceable
    {

        public EntranceDto EntranceDto { get; set; }



        [NotMapped]
        public Park.Devices.Interfaces.IControlable Controlable { get; private set; }

        public override void InitDevice(SDKControlParametes controlParametes)
        {
            switch (EquipmentManufacturers)
            {
                case EquipmentManufacturers.HaiKang:

                    Controlable = new NullDevice();
                    break;

                case EquipmentManufacturers.LanKa:
                    var c = new LanKaControl(controlParametes);
                    Controlable = c;
                    c.MockTest();
                    
                    break;
            }

        }
    }
}
