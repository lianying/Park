using Abp.Domain.Entities;
using Park.Enum;
using Park.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys
{
    public abstract class CameraInfoBase:Entity, ISynchronize
    {

        public virtual string Name { get; set; }

        public virtual string Ip { get; set; }

        public virtual long Port { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }

        public virtual EquipmentManufacturers EquipmentManufacturers { get; set; }


        [ForeignKey("EntranceId")]
        public virtual ParkEntrances.ParkEntrances ParkEntrance { get; set; }


        public virtual long EntranceId { get; set; }

        [NotMapped]
        public DeviceType DeviceType { get; set; }
        [NotMapped]

        public int Sort { get; set; }

        [NotMapped]
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
        public virtual bool IsSuccess { get; set; }
        public virtual string CloudId { get; set; }
    }
}
