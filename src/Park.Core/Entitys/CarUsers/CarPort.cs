using Abp.Domain.Entities.Auditing;
using Park.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Park.Entitys.CarUsers
{
    public class CarPort: FullAuditedEntity<long>, ISynchronize
    {
        /// <summary>
        /// 车位编号
        /// </summary>
        public virtual string CarportSerial { get; set; }

        public virtual long AreaId { get; set; }

        [ForeignKey("AreaId")]
        public virtual ParkAreas.ParkAreas ParkArea { get; set; }

        public virtual long LevelId { get; set; }
        
        [ForeignKey("LevelId")]
        public virtual ParkLevels.ParkLevels ParkLevel { get; set; }

        public virtual DateTime StartTime { get; set; }

        public virtual DateTime EntTime { get; set; }

        /// <summary>
        /// 是否出租
        /// </summary>
        public virtual bool IsRent { get; set; }

        [ForeignKey("CarPortTypeId")]
        public virtual CarTypes.CarTypes CarPortType { get; set; }
        
        public virtual long CarPortTypeId { get; set; }
        /// <summary>
        /// 是否分享
        /// </summary>
        public virtual bool IsSharing { get; set; }

        /// <summary>
        /// 是否有充电桩
        /// </summary>
        public virtual bool HasChargingPile { get; set; }

        /// <summary>
        /// 真实车位
        /// </summary>
        public virtual bool IsRealCarports { get; set; }

        [ForeignKey("CarUserId")]
        public virtual CarUsers CarUser { get; set; }

        
        public virtual long CarUserId { get; set; }
        public virtual bool IsSuccess { get ; set ; }
        public virtual string CloudId { get ; set ; }
    }
}
