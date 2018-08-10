using Park.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys
{
    public abstract class CameraInfoBase
    {

        public string Ip { get; set; }

        public long Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public EquipmentManufacturers EquipmentManufacturers { get; set; }


        public DeviceType DeviceType { get; set; }

        public int Sort { get; set; }
        /// <summary>
        /// 登录ID
        /// </summary>
        public long LoginId { get; set; }

        public abstract void InitDevice(SDKControlParametes controlParametes);
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
