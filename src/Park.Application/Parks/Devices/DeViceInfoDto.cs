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

namespace Park.Devices.Models
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

        /// <summary>
        /// 登录ID
        /// </summary>
        public long LoginId { get; set; }

        public virtual void InitDevice()
        {

        }

        [NotMapped]
        public Park.Devices.Interfaces.IControlable Controlable { get; private set; }
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
        public Stream Image { get; set; }


    }
}
