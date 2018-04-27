using Abp.AutoMapper;
using Park.Entitys.Cameras;
using Park.Enum;
using Park.Parks.Devices.Interfaces;
using Park.Parks.Entrance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Parks.Devices
{
    [AutoMap(typeof(Device))]
    public  class DeviceInfoDto: IDeviceable
    {
        public string Ip { get; set; }

        public long Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public EquipmentManufacturers EquipmentManufacturers { get; set; }

        public EntranceDto EntranceDto { get; set; }

        public DeviceType DeviceType { get; set; }

        public int Sort { get; set; }

        public virtual void InitDevice()
        {

        }

        [NotMapped]
        public IControlable Controlable { get; private set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        [NotMapped]
        public DeviceStatus DeviceStatus { get; set; }
        /// <summary>
        /// 客户数据
        /// </summary>
        [NotMapped]
        public object CalBackData { get; set; }

        /// <summary>
        /// 实时监控中fromHandler
        /// </summary>
        [NotMapped]
        public IntPtr? Handler { get; set; }

        /// <summary>
        /// 拍照图片
        /// </summary>
        [NotMapped]
        public Image Image { get; set; }


    }
}
