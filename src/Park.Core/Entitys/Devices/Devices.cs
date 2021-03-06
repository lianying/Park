﻿using Abp.Domain.Entities;
using Park.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Park.Entitys.Cameras
{
    public class Device:Entity<long>,IMayHaveTenant
    {
        public virtual string Ip { get; set; }

        public virtual long Port { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Password { get; set; }
        /// <summary>
        /// 设备厂商
        /// </summary>
        public virtual EquipmentManufacturers EquipmentManufacturers { get; set; }

        [ForeignKey("EntranceId")]
        public virtual ParkEntrances.ParkEntrances ParkEntrance { get; set; }
        
        public virtual long? EntranceId { get; set; }

        public virtual int? TenantId { get; set; }

        public virtual DeviceType DeviceType { get; set; }

        /// <summary>
        /// 摄像机在岗亭展示的顺序
        /// </summary>
        public virtual int Sort { get; set; }

    }
}
